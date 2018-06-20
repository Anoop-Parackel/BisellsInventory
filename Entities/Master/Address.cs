using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Master
{
    public class Address
    {
        #region Properties
        public string Salutation { get; set; }
        public string ContactName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Zipcode { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        /// <summary>
        /// Address ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Supplier or Customer ID
        /// </summary>
        public int PartyID { get; set; }
        public string Email { get; set; }
        #endregion
    }
}
