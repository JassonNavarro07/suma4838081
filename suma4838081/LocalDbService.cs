using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace suma4838081
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection =  new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            _connection.CreateTableAsync<Resultado>();
        }

        //metodo para listar registros en una tabla
        public async Task<List<Resultado>> GetResultado()
        {
            return await  _connection.Table<Resultado>().ToListAsync();
        }

        //metodo para listar los registros por id
        public async Task<Resultado> GetById(int id)
        {
            return await _connection.Table<Resultado>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //metodo para crear registro
        public async Task Create(Resultado resultado)
        {
            await _connection.InsertAsync(resultado);
        }

        //metodo para actualizar 
        public async Task Update(Resultado resultado)
        {
            await _connection.UpdateAsync(resultado);
        }

        //metodo para eliminar 
        public async Task Delete(Resultado resultado)
        {
            await _connection.DeleteAsync(resultado);
        }
    }
}
