
using CommunityToolkit.Mvvm.Input;
using FakeStorePlatzi.Dtos;
using FakeStorePlatzi.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace FakeStorePlatzi.ViewModels
{
    public class FakeStoreViewModel : INotifyPropertyChanged
    {
        private NetworkAccess accestype= Connectivity.Current.NetworkAccess;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ProductDTO> Productos { get; set; }
        public ProductDTO Product { get; set; }=new ProductDTO();
        public List<CategoryDTO> Categories { get; set; }
        public CategoryDTO Category { get; set; }=new CategoryDTO();
        public CategoryDTO SelectedCategory { get; set; } = new();
        FakeStoreApi _api;
        public string FirstImage;

        public ProductDTO NuevoProducto { get; set; } =new ProductDTO();
        public ICommand AgregarCommand => new AsyncCommand(AgregarProducto);
        public ICommand EditarCommand => new AsyncCommand(ActualizarProducto);
        public ICommand EliminarCommand=>new AsyncCommand<int>(EliminarProducto);
        public ICommand MostrarCommand => new AsyncCommand<int>(Mostrar);


        public string Imagen { get; set; }
        public void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public FakeStoreViewModel()
        {
            llenarProductos();
            llenarCategorias();
            
        }

        private async Task Mostrar(int arg)
        {
            Product=await _api.GetProducto(arg);
            SelectedCategory = Product.category;
            Imagen=Product.images.FirstOrDefault();
            OnPropertyChanged(nameof(Imagen));
            OnPropertyChanged(nameof(Product));
            OnPropertyChanged(nameof(SelectedCategory));
        }
        private async Task llenarCategorias()
        {
            if (accestype == NetworkAccess.Internet)
            {
                
                Categories = await _api.GetCategories();
                OnPropertyChanged(nameof(Categories));
            }
            else
            {
                Console.WriteLine("No hay net");
            }
        }

        private async Task llenarProductos()
        {
            if (accestype == NetworkAccess.Internet)
            {
                _api = new ();
              Productos =new ObservableCollection<ProductDTO> (await _api.GetProductos());
                OnPropertyChanged(nameof(Productos));
            }
            else
            {
                Console.WriteLine("No hay net");
            }
        }

        public async Task AgregarProducto()
        {
            
            NuevoProducto.images.Add(Imagen);
            NuevoProducto.images.Add(Imagen);

          NuevoProducto.categoryId=Category.id;
            
            var nuevoProducto = await _api.Agregar(NuevoProducto);
            if (nuevoProducto != null)
            {
                nuevoProducto.updatedAt = DateTime.Now;
                nuevoProducto.creationAt = DateTime.Now;
                Productos.Add(nuevoProducto);
                OnPropertyChanged(nameof(Productos));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public async Task ActualizarProducto()
        {
            Product.categoryId = Product.category.id;
            Product.images.Clear();
            Imagen = Imagen.Replace("[", "");
            Imagen = Imagen.Substring(1,Imagen.Length-2);
            Product.images.Add(Imagen);
            


            var productoActualizado = await _api.Actualizar(Product);
            if (productoActualizado != null)
            {
                var productoencontrado=Productos.FirstOrDefault(p=>p.id==productoActualizado.id);
                var index = Productos.IndexOf(productoencontrado);
                Productos[index] = productoActualizado;
                OnPropertyChanged(nameof(Productos));
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        public async Task EliminarProducto(int productId)
        {
            var productoEliminado = Productos.FirstOrDefault(p => p.id == productId);
            var eliminado = await _api.Eliminar(productId);
            
            if (eliminado)
            {
                
                if (productoEliminado != null)
                {
                    Productos.Remove(productoEliminado);
                    OnPropertyChanged(nameof(Productos));
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
            }
        }


    }
}
