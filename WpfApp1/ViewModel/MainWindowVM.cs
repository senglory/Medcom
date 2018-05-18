using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using Microsoft.Practices.Unity;
using Medcom.VM;
using Medcom.VM.DTO;
using BLL;


namespace WpfApp1
{
    public class MainWindowVM: NotifyUIBase
    {
        static UnityContainer _container;

        IBLL _bll;

        int _currentOffset = 0;
        ObservableCollection<BaseObjDto> _allrecs;
        public ObservableCollection<BaseObjDto> AllRecords
        {
            get { return _allrecs; }
            set {
                _allrecs = value;
                RaisePropertyChanged("AllRecords");
            }
        }

        BaseObjDto _selectedRec;
        public BaseObjDto SelectedRec
        {
            get
            {
                return _selectedRec;
            }
            set
            {
                if (_selectedRec != value)
                {
                    _selectedRec = value;
                    RaisePropertyChanged("SelectedRec");
                }
            }
        }

        string _storagepath;
        public string StoragePath
        {
            get { return _storagepath; }
            set
            {
                _storagepath = value;
                RaisePropertyChanged("StoragePath");
            }
        }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand AddNoteCommand { get; set; }
        public RelayCommand AddWebUrlCommand { get; set; }
        public RelayCommand AddCreditCardCommand { get; set; }
        public RelayCommand PrevCommand { get; set; }
        public RelayCommand NextCommand { get; set; }
        public RelayCommand OpenCommand { get; set; }


        static MainWindowVM()
        {
            _container = new UnityContainer();
            _container.RegisterType<IBLL, BLL.BLL>();
        }

        public MainWindowVM()
        {
            StoragePath = @"c:\tmp\1";
            _bll = _container.Resolve<IBLL>(new ResolverOverride[]
                {
                    new ParameterOverride("filePath", StoragePath)
                });
            var lst = _bll.GetFiltered();
            PopulateFilteredList(lst);

            SearchCommand = new RelayCommand(DoSearch);
            SaveCommand = new RelayCommand(DoSave);
            CancelCommand = new RelayCommand(DoCancel);
            AddNoteCommand = new RelayCommand(DoAddNote);
            AddWebUrlCommand = new RelayCommand(DoAddWebUrl);
            AddCreditCardCommand = new RelayCommand(DoAddCreditCard);
            PrevCommand = new RelayCommand(GetPrev);
            NextCommand = new RelayCommand(GetNext);
            OpenCommand = new RelayCommand(OpenStorage);
        }

        private void PopulateFilteredList(IEnumerable<BaseObj> lst)
        {
            var people = new ObservableCollection<BaseObjDto>();
            foreach (var o in lst)
            {
                if (o.GetType() == typeof(Note))
                    people.Add(ModuleAModule.TheMapper.Map<NoteDto>(o));
                if (o.GetType() == typeof(CreditCard))
                    people.Add(ModuleAModule.TheMapper.Map<CreditCardDto>(o));
                if (o.GetType() == typeof(WebAcc))
                    people.Add(ModuleAModule.TheMapper.Map<WebAccDto>(o));
            }
            AllRecords = people;
        }

        void DoSearch(object parameter)
        {
            if (parameter == null) return;
            _currentOffset = 0;
            var lst = _bll.GetFiltered((string)parameter, 10, _currentOffset);
            PopulateFilteredList(lst);
        }

        void DoAddNote(object parameter)
        {
            SelectedRec = new NoteDto();
        }
        void DoAddWebUrl(object parameter)
        {
            SelectedRec = new WebAccDto();
        }
        void DoAddCreditCard(object parameter)
        {
            SelectedRec = new CreditCardDto();
        }

        void DoSave(object parameter)
        {
            var ctl = parameter as ContentControl;
            var o = ctl.Content as BaseObjDto;

            if (o.Id == 0)
            {
                BaseObj objNew = null;
                objNew = ObjFromDto(o, objNew);
                _bll.Add(objNew);
            }
            else
            {
                var objExisting = _bll.GetItemById(o.Id);
                ModuleAModule.TheMapper.Map( o, objExisting);

            }
            _bll.Flush();

            _bll = _container.Resolve<IBLL>(new ResolverOverride[]
                {
                    new ParameterOverride("filePath", StoragePath)
                });
            var lst = _bll.GetFiltered("", 10, _currentOffset);
            PopulateFilteredList(lst);
        }

        private static BaseObj ObjFromDto(BaseObjDto o, BaseObj objNew)
        {
            if (o.GetType() == typeof(NoteDto))
                objNew = ModuleAModule.TheMapper.Map<Note>(o);
            if (o.GetType() == typeof(CreditCardDto))
                objNew = ModuleAModule.TheMapper.Map<CreditCard>(o);
            if (o.GetType() == typeof(WebAccDto))
                objNew = ModuleAModule.TheMapper.Map<WebAcc>(o);
            return objNew;
        }

        void DoCancel(object parameter)
        {
            SelectedRec = null;
        }
        void GetPrev(object parameter)
        {
            if (_currentOffset > 0)
                _currentOffset -= 10;
            var lst = _bll.GetFiltered("", 10, _currentOffset);
            PopulateFilteredList(lst);
        }
        void GetNext(object parameter)
        {
            _currentOffset += 10;
            var lst = _bll.GetFiltered("", 10, _currentOffset);
            PopulateFilteredList(lst);
        }

        void OpenStorage(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            var b = openFileDialog.ShowDialog();
            if (b.HasValue && b.Value  ) {
                _currentOffset = 0;
                StoragePath = openFileDialog.FileName;
                _bll = _container.Resolve<IBLL>(new ResolverOverride[]
                    {
                    new ParameterOverride("filePath", StoragePath)
                    });
                var lst = _bll.GetFiltered("", 10, _currentOffset);
                PopulateFilteredList(lst);
            }
        }
    }
}
