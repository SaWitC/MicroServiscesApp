using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class CostomisationModel
    {
        public int Id { get; set; }

        //Regex regex = new Regex();
        //r'#[0-9A-F]{6}'
        [RegularExpression(@"#[0-9A-F]{3|6}")]
        public string BgColor { get; set; }
        [RegularExpression(@"#[0-9A-F]{3|6}")]
        public string ButtonColor { get; set; }
        [RegularExpression(@"#[0-9A-F]{3|6}")]
        public string TextColor { get; set; }
        [RegularExpression(@"#[0-9A-F]{3|6}")]
        public string ButtonTextColor { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        [Required]
        string UserId { get; set; }


    }
}
