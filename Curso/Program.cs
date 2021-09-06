﻿using System;
using System.Linq;
using Curso.Data;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //  using var db = new ApplicationContext();
            // db.Database.Migrate();
            // var exists = db.Database.GetPendingMigrations().Any(); 
            // InserirDados();
            InserirDadosEmMassa();
        }

        private static void ConsultarDados()
        {
            using var db = new ApplicationContext();
            var clientes = db.Clientes.Where(x => x.Id > 0).ToList();

            // Desta forma os objetos não serão rastreados pelo EF e o método Find não
            // irá conseguir encontrar os registros na variável clientes
            // var clientes = db.Clientes.AsNoTracking().Where(x => x.Id > 0).ToList();

            foreach (var cliente in clientes)
            {
                // O método find é o único que faz uso da consulta em memória primeiro 
                // para depois ir buscar no BD
                // Como a variável clientes é resultado de uma consulta sem usar "AsNoTracking", o comando
                // Find irá encontrar o objeto em memória e não precisará efetuar uma consulta ao BD
                db.Clientes.Find(cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto 2",
                CodigoBarras = "123567891",
                Valor = 5m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Diego",
                Telefone = "99998888",
                Estado = "SP",
                Cidade = "Guarulhos",
                CEP = "07055040"
            };

            using var db = new ApplicationContext();

            db.AddRange(produto, cliente);
            var registro = db.SaveChanges();

            Console.WriteLine($"Total de linhas afetadas: {registro}");
        }
        private static void InserirDados(){
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "123567891230",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new ApplicationContext();

            //formas de se adicionar uma entidade para ser salva no banco
            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            // db.Add(produto);

            // retorna o total de linhas afetadas
            var registro = db.SaveChanges();

            Console.WriteLine($"Total de linhas afetadas: {registro}");
        }
    }
}
