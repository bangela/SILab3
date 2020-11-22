using Acr.UserDialogs;
using MvvmCross.Base;
using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.FilePicker;
using SI.Core.Abstract;
using SI.Core.Helpers;
using SI.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SI.Core.ViewModels
{
    public class FirstPageViewModel : MvxNavigationViewModel
    {
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxMainThreadAsyncDispatcher _dispatcher;
        private readonly IDSAService _DSAService;

        public FirstPageViewModel(IMvxLogProvider provider, IMvxNavigationService navigationService, IUserDialogs userDialogs,
            IMvxMainThreadAsyncDispatcher dispatcher, IDSAService dSAService) : base(provider, navigationService)
        {
            _userDialogs = userDialogs;
            _dispatcher = dispatcher;
            _DSAService = dSAService;
        }
        //Q, P, G, Y, X
        #region Properties

        private byte[] _data;
        private string _q;

        public string Q
        {
            get => _q;
            set => SetProperty(ref _q, value);
        }

        private string _p;

        public string P
        {
            get => _p;
            set => SetProperty(ref _p, value);
        }

        private string _g;

        public string G
        {
            get => _g;
            set => SetProperty(ref _g, value);
        }

        private string _x;

        public string X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private string _y;

        public string Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        private string _signature;

        public string Signature
        {
            get => _signature;
            set => SetProperty(ref _signature, value);
        }

        private string _filePath;

        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        #endregion

        #region Commands
        private IMvxCommand _generateCommand;
        public IMvxCommand GenerateCommand => _generateCommand ?? (_generateCommand = new MvxCommand(async () => await Generate()));

        private IMvxCommand _verifyCommand;
        public IMvxCommand VerifyCommand => _verifyCommand ?? (_verifyCommand = new MvxCommand(Verify));

        private IMvxCommand _signCommand;
        public IMvxCommand SignCommand => _signCommand ?? (_signCommand = new MvxCommand(Sign));

        private IMvxCommand _chooseFileCommand;
        public IMvxCommand ChooseFileCommand => _chooseFileCommand ?? (_chooseFileCommand = new MvxCommand(async () => await ChooseFile()));

        private IMvxCommand _chooseSignatureCommand;
        public IMvxCommand ChooseSignatureCommand => _chooseSignatureCommand ?? (_chooseSignatureCommand = new MvxCommand(ChooseSignature));
        #endregion

        #region Private methods
        private void Sign()
        {
            var sign = _DSAService.SignData(_data);
            var str = DsaSerializer.Serialize(sign);
            var bytes = Encoding.ASCII.GetBytes(str);
            var base64 = Convert.ToBase64String(bytes);
            Signature = base64;
        }

        private void Verify()
        {
            if (string.IsNullOrEmpty(_signature))
            {
                _userDialogs.Alert("No signature", "Error");
                return;
            }
            if (string.IsNullOrEmpty(_filePath))
            {
                _userDialogs.Alert("No file", "Error");
                return;
            }
            var isVerfied = false;
            try
            {
                var bytes = Convert.FromBase64String(_signature);
                var str = Encoding.ASCII.GetString(bytes);
                var sign = DsaSerializer.Deserialize(str);
                isVerfied = _DSAService.Verify(_data, sign.Item1, sign.Item2);
            }
            catch (Exception e)
            {
                isVerfied = false;
            }
            if (isVerfied)
            {
                _userDialogs.Alert("Is valid", "Response");
            }
            else
            {
                _userDialogs.Alert("Is invalid", "Response");
            }
        }

        private async Task ChooseFile()
        {
            var fileData = await CrossFilePicker.Current.PickFile();
            _data = fileData.DataArray;
            FilePath = fileData.FilePath;
        }

        private void ChooseSignature()
        {
            //var fileData = await CrossFilePicker.Current.PickFile();
            //var data = System.Text.Encoding.Default.GetString(fileData.DataArray);
            //var bytearray = Convert.FromBase64String(data);
            //var str = System.Text.Encoding.Default.GetString(bytearray);
            //Signature = str;
        }
        private async Task Generate()
        {
            using (_userDialogs.Loading("Generate keys"))
            {
                await Task.Run(async () => _DSAService.GenerateKey());
                _DSAService.GenerateKey();
                Q = _DSAService.Q.ToString();
                P = _DSAService.P.ToString();
                G = _DSAService.G.ToString();
                X = _DSAService.X.ToString();
                Y = _DSAService.Y.ToString();
            }
        }
        #endregion
    }
}
