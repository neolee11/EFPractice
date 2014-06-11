using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFPractice.Domain
{
    public class ContactDetail
    {
        //public int Id { get; set; }
        //[Key, ForeignKey("Customer")]
        public int CustomerId { get; set; }

        //   [Column("CellPhone")]
        public string MobilePhone { get; set; }

        //[MaxLength(20)]
        public string HomePhone { get; set; }
        public string OfficePhone { get; set; }
        public string TwitterAlias { get; set; }
        public string Facebook { get; set; }
        public string LinkedIn { get; set; }
        public string Skype { get; set; }
        public string Messenger { get; set; }
        public string TestProp { get; set; }

        public virtual Customer Customer { get; set; }
    }
}