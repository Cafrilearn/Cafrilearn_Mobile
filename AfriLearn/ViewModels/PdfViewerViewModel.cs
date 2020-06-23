using AfriLearn;
using AfriLearn.Services;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace AfriLearn.ViewModels
{
    class PdfViewerViewModel : BaseViewModel
    {
        private Stream m_pdfDocumentStream;

        public Stream PdfDocumentStream
        {
            get => m_pdfDocumentStream;
            set
            {
                m_pdfDocumentStream = value;
                OnPropertyChanged(nameof(PdfViewerViewModel));
            }
        }
       
        public PdfViewerViewModel()
        {
            
            PdfDocumentStream = AzureBlobStorageService
                                .GetBlobAsync("english", "ENGLISH GRAMMAR PART 1.pdf").Result;

        }

       
    }
}