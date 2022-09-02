using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace APICursosGratuitos.Utils
{
    public static class Upload  // Um Singleton sempre será uma classe estática
    {
        // Upload de Imagens
        public static string UploadFile(IFormFile arquivo, string[] extensoesPermitidas, string diretorio)
        {

            try
            {
                // Determinamos onde será salvo o arquivo
                var pasta = Path.Combine("StaticFiles", diretorio);
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                // Verificamos se existe um arquivo para ser salvo
                if (arquivo.Length >= 0)
                {
                    // Pegamos o nome do arquivo
                    string nomeArquivo = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    // Validamos se a extensão é permitida
                    if (ValidarExtensao(extensoesPermitidas, nomeArquivo))
                    {
                        var extensao = RetornarExtensao(nomeArquivo);
                        var novoNome = $"{Guid.NewGuid()}.{extensao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        // Salvamos efetivamente o arquivo na aplicação
                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }

                        return novoNome;

                    }
                }
                return "";
            }
            catch (System.Exception e)
            {

                return e.Message;
            }

        }


        // Validar extensão de arquivo
        public static bool ValidarExtensao(string[] extensoesPermitidas, string nomeArquivo)
        {
            //string[] dados = nomeArquivo.Split('.');

            string extensao = RetornarExtensao(nomeArquivo);

            foreach (string ext in extensoesPermitidas)
            {
                if (ext == extensao)
                {
                    return true;
                }
            }

            return false;
        }



        // Retornar a extensão
        public static string RetornarExtensao(string nomeArquivo)
        {
            //[0] [1] [2]
            //arq.uivo.jpeg = 3
            //lenght(3) -1 = 2
            string[] dados = nomeArquivo.Split('.'); //retorna uma array de strings gerada pela divisão da string original separada pelo valor que passa como parâmetro 
            return dados[dados.Length - 1];

        }
    }
}
