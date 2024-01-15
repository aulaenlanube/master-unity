using UnityEngine;

public class Comerciante : IComerciante
{
    private Inventario inventario;
    private string nombre;
    private int oro;
    private int plata;
    private int bronce;

    public string Nombre
    {
        get => nombre;
        private set => nombre = value;
    }
    public int Oro
    {
        get => oro;
        private set => oro = Mathf.Max(0, value);
    }
    public int Plata
    {
        get => plata;
        private set => plata = Mathf.Max(0, value);
    }
    public int Bronce
    {
        get => bronce;
        private set => bronce = Mathf.Max(0, value);
    }

    public Comerciante(string nombre)
    {
        Nombre = nombre;
        inventario = new Inventario();
        oro = 0;
        plata = 0;
        bronce = 0;
    }
    public Comerciante(string nombre, Inventario inventario)
    {
        Nombre = nombre;
        this.inventario = inventario;
        oro = 0;
        plata = 0;
        bronce = 0;
    }
    public Comerciante(string nombre, Inventario inventario, int oro, int plata, int bronce)
    {
        Nombre = nombre;
        this.inventario = inventario;
        this.oro = oro;
        this.plata = plata;
        this.bronce = bronce;
    }

    public bool DineroSuficienteCompra(int oro, int plata, int bronce)
    {
        //1 de oro = 10 de plata        
        //1 de plata = 10 de bronce
        return this.oro * 100 + this.plata * 10 + this.bronce >= oro * 100 + plata * 10 + bronce;
    }

    public void Comprar(IComerciable objeto)
    {
        //si es un material básico, se añade de forma distinta
        if (objeto is MaterialBasico materialBasico)
        {
            Debug.Log($"ERROR: Para comprar un material básico se debe indicar la cantidad de unidades.");
            return;
        }

        //si no hay dinero suficiente, no se puede comprar
        if (objeto is ObjetoInventario objInventario && !DineroSuficienteCompra(objInventario.CosteOro, objInventario.CostePlata, objInventario.CosteBronce))
        {
            Debug.Log($"No se puede comprar {objInventario.Nombre} porque no hay suficiente dinero");
            return;
        }

        //si es un objeto de inventario, se añade al inventario
        if (objeto is ObjetoInventario objetoInventario)
        {
            objeto.Comprar();
            RestarSaldo(objetoInventario.CosteOro, objetoInventario.CostePlata, objetoInventario.CosteBronce);
            inventario.AgregarObjeto(objetoInventario);
        }
        else
        {
            Debug.Log("No se puede comprar el objeto");
        }

        MostrarSaldo();
    }

    public void Vender(IComerciable objeto)
    {
        //si es un material básico, se vende de forma distinta
        if (objeto is MaterialBasico materialBasico)
        {
            Debug.Log($"ERROR: Para vender un material básico se debe indicar la cantidad de unidades.");
            return;
        }

        //si es un objeto de inventario, se elimina del inventario
        if (objeto is ObjetoInventario objetoInventario)
        {
            //si el objeto no está en el inventario
            if (!inventario.EstaEnInventario(objetoInventario))
            {
                Debug.Log($"No se puede vender {objetoInventario.Nombre} porque no está en el inventario");
                return;
            }
            //si el objeto está en el inventario, se vende
            objeto.Vender();
            SumarSaldo(objetoInventario.CosteOro, objetoInventario.CostePlata, objetoInventario.CosteBronce);
            inventario.EliminarObjeto(objetoInventario);            
        }
        else
        {
            Debug.Log("No se puede vender el objeto");
        }

        MostrarSaldo();
    }

    public void ListarInventario()    
    {     
        inventario.MostrarInventario();    
    } 
    
    public bool MaterialSuficienteVenta(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        return inventario.ObtenerCantidadMaterialBasico(tipoMaterialBasico) >= cantidad;
    }

    public void ComprarMaterialBasico(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        int costeTotalOro = PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteOro * cantidad;
        int costeTotalPlata = PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CostePlata * cantidad;
        int costeTotalBronce = PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteBronce * cantidad;        
        
        if(DineroSuficienteCompra(costeTotalOro, costeTotalPlata, costeTotalBronce))
        {             
            inventario.AgregarMaterialBasico(tipoMaterialBasico, cantidad);
            RestarSaldo(tipoMaterialBasico, cantidad);
            inventario.ObtenerMaterialBasico(tipoMaterialBasico)?.Comprar(); 
            return;
        }
        Debug.Log($"No se puede comprar {tipoMaterialBasico} porque no hay suficiente dinero");                
    }

    public void VenderMaterialBasico(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {        
        if (MaterialSuficienteVenta(tipoMaterialBasico, cantidad))
        {            
            inventario.EliminarMaterialBasico(tipoMaterialBasico, cantidad);
            SumarSaldo(tipoMaterialBasico, cantidad);
            inventario.ObtenerMaterialBasico(tipoMaterialBasico)?.Vender();
            return;
        }
        Debug.Log($"No se puede vender {tipoMaterialBasico} porque no hay suficientes unidades");        
    }   

    private void SumarSaldo(int oro, int plata, int bronce)
    {
        int totalSumar = oro * 100 + plata * 10 + bronce;
        int totalBronce = this.oro * 100 + this.plata * 10 + this.bronce + totalSumar;
        AjustarMonedas(totalBronce);
    }

    private void SumarSaldo(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        SumarSaldo(PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteOro * cantidad,
                   PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CostePlata * cantidad,
                   PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteBronce * cantidad);       
    }

    private void RestarSaldo(int oro, int plata, int bronce)
    {
        int totalRestar = oro * 100 + plata * 10 + bronce;
        int totalBronce = this.oro * 100 + this.plata * 10 + this.bronce - totalRestar;
        AjustarMonedas(totalBronce);
    }

    private void RestarSaldo(TipoMaterialBasico tipoMaterialBasico, int cantidad)
    {
        RestarSaldo(PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteOro * cantidad,
                    PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CostePlata * cantidad,
                    PreciosMaterialesBasicos.ObtenerPrecio(tipoMaterialBasico).CosteBronce * cantidad);
    }

    private void AjustarMonedas(int totalBronce)
    {
        this.oro = totalBronce / 100;
        this.plata = totalBronce % 100 / 10;
        this.bronce = totalBronce % 10;
    }

    public void MostrarSaldo()
    {
        Debug.Log($"Saldo actual: {oro} de oro, {plata} de plata y {bronce} de bronce");
    }
}
