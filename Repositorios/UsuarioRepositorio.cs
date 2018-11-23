using System;
using System.Collections.Generic;
using System.IO;
using Senai.Financas.Web.Mvc.Models;
using Senai_Financas_Mvc_Web_master.Interfaces;

namespace Senai_Financas_Mvc_Web_master.Repositorios
{
    public class UsuarioRepositorio : IUsuario
    {

        public UsuarioModel BuscarPorId(int id)
        {
            string[] linhas = System.IO.File.ReadAllLines("usuarios.csv");

            foreach (var item in linhas)
            {
                string[] dados = item.Split(';');

                if (id.ToString() == dados[0]){
                    UsuarioModel usuario = new UsuarioModel(
                                            id: int.Parse(dados[0]),
                                            nome: dados[1],
                                            email: dados[2],
                                            senha: dados[3],
                                            dataNascimento: DateTime.Parse(dados[4])
                                        );

                    return usuario;
                }
            }

            return null;
        }

        public UsuarioModel Cadastrar(UsuarioModel usuario)
        {
            //Verifica se o arquivo existe
            if(File.Exists("usuarios.csv")){
                //Se arquivo existe Pega a quantidade de linhas e incrementa 1
                usuario.Id = File.ReadAllLines("usuarios.csv").Length + 1;
            } else {
                //caso não exista seta como 1
                usuario.Id = 1;
            }

            //Grava as informações no arquivo usuarios.csv
            using (StreamWriter sw = new StreamWriter ("usuarios.csv", true)) {
                sw.WriteLine ($"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha};{usuario.DataNascimento}");
            }

            return usuario;
        }

        public UsuarioModel Editar(UsuarioModel usuario)
        {
            string[] linhas = System.IO.File.ReadAllLines("usuarios.csv");

            for (int i = 0; i < linhas.Length; i++)
            {
                if(string.IsNullOrEmpty(linhas[i])){
                    continue;
                }

                string[] dados = linhas[i].Split(';');

                //Verifica se o id do formulário é igual ao da linha
                if(usuario.Id.ToString() == dados[0]){
                    //Altera os dados da linha
                    linhas[i] = $"{usuario.Id};{usuario.Nome};{usuario.Email};{usuario.Senha};{usuario.DataNascimento}";
                    break;
                }
            }

            //Altera o arquivo usuarios.csv
            System.IO.File.WriteAllLines("usuarios.csv", linhas);

            return usuario;
        }

        public void Excluir(int id)
        {
            //Pega os dados do arquivo usuario.csv
            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            //Percorre as linhas do arquivo
            for (int i = 0; i < linhas.Length; i++) {
                //Separa as colunas da linha
                string[] linha = linhas[i].Split (';');

                //Verifica se o id da linha é o id passado
                if (id.ToString () == linha[0]) {
                    //Defino a linha como vazia
                    linhas[i] = "";
                    break;
                }
            }

            //Armazeno no arquivo csv todas as linhas
            System.IO.File.WriteAllLines ("usuarios.csv", linhas);
        }

        public List<UsuarioModel> Listar()
        {
            List<UsuarioModel> lsUsuarios = new List<UsuarioModel> ();

            string[] linhas = System.IO.File.ReadAllLines ("usuarios.csv");

            UsuarioModel usuario;

            foreach (var item in linhas) {

                //Verifica se a linha é vazia
                if (string.IsNullOrEmpty (item)) {
                    //Retorna para o foreach
                    continue;
                }

                string[] linha = item.Split (';');

                usuario = new UsuarioModel (
                                            id: int.Parse (linha[0]),
                                            nome: linha[1],
                                            email: linha[2],
                                            senha: linha[3],
                                            dataNascimento: DateTime.Parse (linha[4])
                                        );

                lsUsuarios.Add (usuario);
            }

            return lsUsuarios;
        }

        public UsuarioModel Login(string email, string senha)
        {
            using (StreamReader sr = new StreamReader ("usuarios.csv")) {
                while (!sr.EndOfStream) {
                    var linha = sr.ReadLine ();

                    if (string.IsNullOrEmpty(linha))
                    {
                        continue;
                    }

                    string[] dados = linha.Split (";");

                    if (dados[2] == email && dados[3] == senha) {
                        UsuarioModel usuario = new UsuarioModel(
                            id: int.Parse (dados[0]),
                            nome: dados[1],
                            email: dados[2],
                            senha: dados[3],
                            dataNascimento: DateTime.Parse (dados[4])
                        );

                        return usuario;
                    }
                }
            }

            return null;
        }
    }
}