﻿using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Dev2.Activities.Designers2.Core.CloneInputRegion;
using Dev2.Common.Interfaces.DB;
using Dev2.Common.Interfaces.ToolBase;
using Dev2.Common.Interfaces.ToolBase.Database;
using Dev2.Studio.Core.Activities.Utils;
using Microsoft.Practices.Prism;
using Warewolf.Core;

namespace Dev2.Activities.Designers2.Core.InputRegion
{
    public sealed class DatabaseInputRegion : IDatabaseInputRegion
    {
        private readonly IActionInputDatatalistMapper _datatalistMapper;
        private readonly ModelItem _modelItem;
        private readonly IActionToolRegion<IDbAction> _action;
        bool _isEnabled;

        private ICollection<IServiceInput> _inputs;
        private bool _isInputsEmptyRows;

        public DatabaseInputRegion()
        {
            ToolRegionName = "DatabaseInputRegion";
        }

        public DatabaseInputRegion(ModelItem modelItem, IActionToolRegion<IDbAction> action)
            : this(new ActionInputDatatalistMapper())
        {
            ToolRegionName = "DatabaseInputRegion";
            _modelItem = modelItem;
            _action = action;
            _action.SomethingChanged += SourceOnSomethingChanged;
            var inputsFromModel = _modelItem.GetProperty<ICollection<IServiceInput>>("Inputs");
            var serviceInputs = inputsFromModel ?? new List<IServiceInput>();
            var inputs = new ObservableCollection<IServiceInput>();
            inputs.CollectionChanged += InputsCollectionChanged;
            inputs.AddRange(serviceInputs);
            Inputs = inputs;
            if (inputsFromModel == null)
            {
                UpdateOnActionSelection();
            }

            IsEnabled = _action?.SelectedAction != null;
        }

        private void InputsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            AddItemPropertyChangeEvent(e);
            RemoveItemPropertyChangeEvent(e);
        }

        private void AddItemPropertyChangeEvent(NotifyCollectionChangedEventArgs args)
        {
            if (args.NewItems == null)
            {
                return;
            }

            foreach (INotifyPropertyChanged item in args.NewItems)
            {
                if (item != null)
                {
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _modelItem.SetProperty("Inputs", Inputs);
        }

        private void RemoveItemPropertyChangeEvent(NotifyCollectionChangedEventArgs args)
        {
            if (args.OldItems == null)
            {
                return;
            }

            foreach (INotifyPropertyChanged item in args.OldItems)
            {
                if (item != null)
                {
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        public DatabaseInputRegion(IActionInputDatatalistMapper datatalistMapper)
        {
            _datatalistMapper = datatalistMapper;
        }

        private void SourceOnSomethingChanged(object sender, IToolRegion args)
        {
            try
            {
                Errors.Clear();

                UpdateOnActionSelection();

                OnPropertyChanged(@"Inputs");
                OnPropertyChanged(@"IsEnabled");
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
            }
            finally
            {
                CallErrorsEventHandler();
            }
        }

        private void CallErrorsEventHandler()
        {
            ErrorsHandler?.Invoke(this, new List<string>(Errors));
        }

        private void UpdateOnActionSelection()
        {
            IsEnabled = _action?.SelectedAction != null;
            var inputCopy = Inputs.ToArray().Clone() as ICollection<IServiceInput>;
            Inputs = new List<IServiceInput>();
            if (_action?.SelectedAction != null)
            {
                var selectedActionInputs = _action.SelectedAction.Inputs;
                var selectedAction = ((DbAction)_action.SelectedAction).Name;
                var isTheSameActionWithPrevious = inputCopy.All(input => input.ActionName?.Equals(selectedAction) ?? false);
                if (inputCopy.Any() && isTheSameActionWithPrevious)
                {

                    var newInputs = InputsFromSameAction(selectedActionInputs);
                    var removedInputs = inputCopy.Except(selectedActionInputs, new ServiceInputNameComparer()).ToList();
                    var union = inputCopy.Union(newInputs, new ServiceInputNameComparer()).ToList();
                    union.RemoveAll(a => removedInputs.Any(k => a.Equals(k)));
                    Inputs = union;
                }
                else
                {
                    Inputs = selectedActionInputs;
                    _datatalistMapper.MapInputsToDatalist(Inputs);
                    IsInputsEmptyRows = Inputs.Count < 1;
                    IsEnabled = true;
                }
            }
            OnPropertyChanged("Inputs");
        }

        private ICollection<IServiceInput> InputsFromSameAction(IList<IServiceInput> selectedActionInputs)
        {
            if (!Inputs.SequenceEqual(selectedActionInputs, new ServiceInputNameValueComparer()))
            {
                var newInputs = selectedActionInputs.Except(Inputs, new ServiceInputNameComparer());
                var serviceInputs = newInputs as IServiceInput[] ?? newInputs.ToArray();
                _datatalistMapper.MapInputsToDatalist(serviceInputs);
                return serviceInputs;
            }
            return new List<IServiceInput>();
        }

        public bool IsInputsEmptyRows
        {
            get
            {
                return _isInputsEmptyRows;
            }
            set
            {
                _isInputsEmptyRows = value;
                OnPropertyChanged();
            }
        }

        #region Implementation of IToolRegion

        public string ToolRegionName { get; set; }
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public IList<IToolRegion> Dependants { get; set; }

        public IToolRegion CloneRegion()
        {
            var inputs2 = new List<IServiceInput>(Inputs);
            return new DatabaseInputRegionClone
            {
                Inputs = inputs2,
                IsEnabled = IsEnabled
            };
        }

        public void RestoreRegion(IToolRegion toRestore)
        {
            if (toRestore is DatabaseInputRegionClone region)
            {
                Inputs.Clear();
                if (region.Inputs != null)
                {
                    var inp = region.Inputs.ToList();

                    Inputs = inp;
                }
                OnPropertyChanged("Inputs");
                IsInputsEmptyRows = Inputs == null || Inputs.Count == 0;
            }
        }

        public EventHandler<List<string>> ErrorsHandler
        {
            get;
            set;
        }

        public IList<string> Errors
        {
            get
            {
                IList<string> errors = new List<string>();
                return errors;
            }
            set
            {
                ErrorsHandler.Invoke(this, new List<string>(value));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Implementation of IDatabaseInputRegion

        public ICollection<IServiceInput> Inputs
        {
            get
            {
                return _inputs;
            }
            set
            {
                if (value != null)
                {
                    _inputs = value;
                    _modelItem.SetProperty("Inputs", value.ToList());
                    OnPropertyChanged();
                }
                else
                {
                    _inputs.Clear();
                    _modelItem.SetProperty("Inputs", _inputs.ToList());
                    OnPropertyChanged();
                }

            }
        }

        #endregion
    }
}
