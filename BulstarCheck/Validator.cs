
namespace BulstarCheck
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class Validator
    {
        private const string LOC = "validation";

        public static void ValidatePerson(string lpk, string fname, string sname, string lastname)
        {
            string namepattern = @"^\w{0,40}|\s+$";
            string fnamepattern = @"^\w{1,40}|\s+$";
            
            if (!Regex.IsMatch(lpk, @"^[\d]{8,10}$"))
            {
                throw new BulstarCheckException(Messages.validation_lpk, LOC);
            }
            if (!Regex.IsMatch(fname, fnamepattern))
            {
                throw new BulstarCheckException(Messages.validation_fname, LOC);
            }
            if (!Regex.IsMatch(sname, namepattern))
            {
                throw new BulstarCheckException(Messages.validation_sname, LOC);
            }
            if (!Regex.IsMatch(lastname, namepattern))
            {
                throw new BulstarCheckException(Messages.validation_lname, LOC);
            }
        }

        public static void ValidateEvent(string name, string description, string date)
        {
            string namepattern = @"^\w{0,40}|\s+$";
            if (!Regex.IsMatch(name, namepattern))
            {
                throw new BulstarCheckException(Messages.validation_event_name, LOC);
            }
            if (!Regex.IsMatch(description, namepattern))
            {
                throw new BulstarCheckException(Messages.validation_event_desc, LOC);
            }
            try
            {
                DateTime d = DateTime.Parse(date, System.Globalization.CultureInfo.CurrentCulture);
            }
            catch (Exception)
            {
                throw new BulstarCheckException(Messages.validation_event_date, LOC);
            }
        }
    }
}