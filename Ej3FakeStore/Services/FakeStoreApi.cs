using FakeStorePlatzi.Dtos;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FakeStorePlatzi.Services
{
    public class FakeStoreApi
    {
        HttpClient _client;
        string _url = "https://api.escuelajs.co/api/v1/products";
        public async Task<List<ProductDTO>> GetProductos()
        {
            _client = new HttpClient();
            var _response = await _client.GetAsync(_url);
            _response.EnsureSuccessStatusCode();
            var _json = await _response.Content.ReadAsStringAsync();
            var _prosuctsdata= JsonConvert.DeserializeObject<List<ProductDTO>>(_json);
            
            return _prosuctsdata;
        }

        public async Task<ProductDTO> GetProducto(int id)
        {
            var response = await _client.GetAsync($"{_url}/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var charactersData = JsonConvert.DeserializeObject<ProductDTO>(json);
            return charactersData;
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            _client = new HttpClient();
            var _response = await _client.GetAsync("https://api.escuelajs.co/api/v1/categories");
            _response.EnsureSuccessStatusCode();
            var _json = await _response.Content.ReadAsStringAsync();
            var _prosuctsdata = JsonConvert.DeserializeObject<List<CategoryDTO>>(_json);

            return _prosuctsdata;
        }

       
        public async Task<ProductDTO> Agregar(ProductDTO productDTO)
        {

            _client = new HttpClient();
            var otrosjson = JsonConvert.SerializeObject(productDTO);
            var content=new StringContent(otrosjson,Encoding.UTF8,"application/json");
            var _response = await _client.PostAsync(_url, content) ;
            _response.EnsureSuccessStatusCode();
            var _json = await _response.Content.ReadAsStringAsync();
            var _prosuctsdata = JsonConvert.DeserializeObject<ProductDTO>(_json);
            return _prosuctsdata;
        }


        public async Task<ProductDTO> Actualizar(ProductDTO productDTO)
        {
            _client = new HttpClient();
            var otrosjson = JsonConvert.SerializeObject(productDTO);
            var content = new StringContent(otrosjson, Encoding.UTF8, "application/json");
            var _response = await _client.PutAsync($"{_url}/{productDTO.id}", content);
            _response.EnsureSuccessStatusCode();
            var _json = await _response.Content.ReadAsStringAsync();
            var _prosuctsdata = JsonConvert.DeserializeObject<ProductDTO>(_json);
            return _prosuctsdata;
        }

        public async Task<bool> Eliminar(int productId)
        {
            _client = new HttpClient();
            var _response = await _client.DeleteAsync($"{_url}/{productId}");
            return _response.IsSuccessStatusCode;
        }

    }
}
