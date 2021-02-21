using System.ComponentModel.DataAnnotations;

namespace Api.Contracts.v1.Requests
{
    public class UpdatePostRequest
    {
        [Required]
        public string Name { get; set; }

    }
}
