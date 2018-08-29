using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zSpaceWinApp.Model
{
    public class LocModel: BaseModel
    {
        private string _Keyword = "";

        public string Keyword
        {
            get { return _Keyword; }
            set { _Keyword = value; OnPropertyChanged(); }
        }

        private string _English = "";

        public string English
        {
            get { return _English; }
            set { _English = value; OnPropertyChanged(); }
        }

        private string _OldEnglish;

        public string OldEnglish
        {
            get { return _OldEnglish; }
            set { _OldEnglish = value; }
        }

        private string _Spanish = "";

        public string Spanish
        {
            get { return _Spanish; }
            set { _Spanish = value; OnPropertyChanged(); }
        }

        private string _OldSpanish = "";

        public string OldSpanish
        {
            get { return _OldSpanish; }
            set { _OldSpanish = value; }
        }

        private string _Chinese = "";

        public string Chinese
        {
            get { return _Chinese; }
            set { _Chinese = value; OnPropertyChanged(); }
        }

        private string _OldChinese;

        public string OldChinese
        {
            get { return _OldChinese; }
            set { _OldChinese = value; }
        }
        
        private string _Japanese = "";

        public string Japanese
        {
            get { return _Japanese; }
            set { _Japanese = value; OnPropertyChanged(); }
        }

        private string _OldJapanese;

        public string OldJapanese
        {
            get { return _OldJapanese; }
            set { _OldJapanese = value; }
        }
        
        private string _Turkish = "";

        public string Turkish
        {
            get { return _Turkish; }
            set { _Turkish = value; OnPropertyChanged(); }
        }

        private string _OldTurkish;

        public string OldTurkish
        {
            get { return _OldTurkish; }
            set { _OldTurkish = value; }
        }
        
    }
}
