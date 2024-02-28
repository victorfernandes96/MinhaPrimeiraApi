using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MinhaPrimeiraApi.Entidades.Entidades
{
    public class Produto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}