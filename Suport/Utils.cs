using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace InoveTeste.Suport
{ 
    public class Utils
    {
        public class GeradorDeDados
        {
            public DadosFalsos Gerar()
            {
                var faker = new Faker("pt_BR"); // "pt_BR" para dados em português

                var dados = new DadosFalsos
                {
                    Nome = faker.Person.FullName,
                    Email = faker.Internet.Email(),
                    Assunto = faker.Lorem.Sentence(5), // Assunto com 5 palavras
                    Mensagem = faker.Lorem.Lines(2) // Duas linhas
                };

                return dados;
            }
        }
            
        public class DadosFalsos
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Assunto { get; set; }
            public string Mensagem { get; set; }

        }
    }     
}