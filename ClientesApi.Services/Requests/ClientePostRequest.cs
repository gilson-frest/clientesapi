using System.ComponentModel.DataAnnotations;

namespace ClientesApi.Services.Requests
{
    public class ClientePostRequest
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Formato do email inválido.")]
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Email { get; set; }
                
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório.")]
        public DateTime DataNascimento { get; set; }
    }
}
