using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HrWebApp1.ViewModels
{
    public class EditFileViewModel
    {
        public int UserId { get; set; }
        public int FileCatsId { get; set; }
        public List<string> Specialization { get; set; }
        public string SpecializationComm { get; set; }
        public List<string> Udv { get; set; }
        public string UdvComm { get; set; }
        public List<string> Pension { get; set; }
        public string PensionComm { get; set; }
        public List<string> Certificates { get; set; }
        public string CertificatesComm { get; set; }
        public List<string> Employhis { get; set; }
        public string EmployhisComm { get; set; }
        public List<string> Addres { get; set; }
        public string AddresComm { get; set; }
        public List<string> Conviction { get; set; }
        public string ConvictionComm { get; set; }
        public List<string> Narcodisp { get; set; }
        public string NarcodispComm { get; set; }
        public List<string> Psychodisp { get; set; }
        public string PsychodispComm { get; set; }
        public List<string> Military { get; set; }
        public string MilitaryComm { get; set; }
        public List<string> Docphoto { get; set; }
        public string DocphotoComm { get; set; }
        public List<string> Refmainjob { get; set; }
        public string RefmainjobComm { get; set; }
        public List<string> Marriage { get; set; }
        public string MarriageComm { get; set; }
        public List<string> Cash { get; set; }
        public string CashComm { get; set; }
        public List<string> Forma086 { get; set; }
        public string Forma086Comm { get; set; }
        public Byte[] FileByte { get; set; }
    }
}
