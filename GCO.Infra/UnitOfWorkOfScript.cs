using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.Composition;
using GCO.Dominio.Entidade;
using GCO.Dominio;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Diagnostics;


namespace GCO.Infra
{
    public class UnitOfWorkOfScript
    {
        private Server server;
        private SqlConnection connection;

        public void BeginTransaction()
        {
            server.ConnectionContext.BeginTransaction();
        }

        public void CommitTransaction()
        {
            server.ConnectionContext.CommitTransaction();

            if (server.ConnectionContext.IsOpen)
                server.ConnectionContext.Disconnect();
        }

        public void RollBackTransaction()
        {
            server.ConnectionContext.RollBackTransaction();

            if (server.ConnectionContext.IsOpen)
                server.ConnectionContext.Disconnect();
        }

        public UnitOfWorkOfScript(string connectionString, string nomeBanco)
        {
            connection = new SqlConnection(connectionString.Replace("Catalog=", "Catalog=" + nomeBanco));
            connection.Open();
            connection.Close();
            server = new Server(new ServerConnection(connection));
        }

        public void ExecutarScript(ScriptDeBanco script)
        {
            try
            {
                this.server.ConnectionContext.ExecuteNonQuery(script.Script, ExecutionTypes.Default);
            }
            catch (Microsoft.SqlServer.Management.Common.ExecutionFailureException exc)
            {
                var excSQL = (SqlException)exc.InnerException;

                throw new Exception("BANCO DE DADOS: " + connection.Database + "  SCRIPT: " + script.Nome + "  LINHA: " + excSQL.LineNumber + "  MENSAGEM: " + exc.InnerException.Message);
            }
        }

        public void ValidarScript(ScriptDeBanco script)
        {
            try
            {
                string database = this.connection.Database;
                this.server.ConnectionContext.ExecuteNonQuery(script.Script, ExecutionTypes.Default);
                if (database != this.connection.Database)
                {
                    throw new Exception("Não permitido alterar o banco de dados.", new Exception("Não permitido alterar o banco de dados."));
                }
            }
            catch (Microsoft.SqlServer.Management.Common.ExecutionFailureException exc)
            {
                var excSQL = (SqlException)exc.InnerException;

                throw new Exception("BANCO DE DADOS: " + connection.Database + "  SCRIPT: " + script.Nome + "  LINHA: " + excSQL.LineNumber + "  MENSAGEM: " + exc.InnerException.Message);
            }
        }

    }
}
