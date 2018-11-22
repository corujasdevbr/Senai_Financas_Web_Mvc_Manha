using System.Collections.Generic;
using Senai.Financas.Web.Mvc.Models;

namespace Senai_Financas_Mvc_Web_master.Interfaces
{
    /// <summary>
    /// Interface do Usuário
    /// Metódos que a classe que irá herdar deverá ter
    /// </summary>
    public interface IUsuario
    {
        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Retorna um List<UsuarioModel></returns>
         List<UsuarioModel> Listar();

        /// <summary>
        /// Cadastrar um novo usuário
        /// </summary>
        /// <param name="usuario">UsuarioModel</param>
        /// <returns>Retorna um usuário</returns>
         UsuarioModel Cadastrar(UsuarioModel usuario);

        /// <summary>
        /// Edita um usuario
        /// </summary>
        /// <param name="usuario">UsuarioModel</param>
        /// <returns>Retorna um UsuarioModel editado</returns>
         UsuarioModel Editar(UsuarioModel usuario);

        /// <summary>
        /// Exclui um usuário por meio do seu Id
        /// </summary>
        /// <param name="id">id do usuário</param>
         void Excluir(int id);

        /// <summary>
        /// Efetua o login do usuário
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns></returns>
         UsuarioModel Login(string email, string senha);

         UsuarioModel BuscarPorId(int id);
    }
}