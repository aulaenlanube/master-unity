using UnityEngine;

public class EjemploInventario : MonoBehaviour
{
    void Start()
    {
        Inventario inventarioPersonajePrincipal = new Inventario();        

        //personaje principal
        Arma espadaDelAmanecer = new Arma("Espada del Amanecer", "Una espada legendaria que brilla con la luz del primer sol", Rareza.Legendario, null, 200, 0, 0, 20.0f, 1.5f, 150, 3.0f, true, true);
        Arma arcoDeSombra = new Arma("Arco de Sombra", "Un arco antiguo forjado en las profundidades oscuras, ligero y letal", Rareza.Epico, null, 150, 10, 5, 12.0f, 2.0f, 120, 5.0f, true, true);
        Arma lanzaDeDestino = new Arma("Lanza del Destino", "Forjada en los fuegos del destino, esta lanza nunca falla su objetivo", Rareza.Raro, null, 100, 5, 5, 18.0f, 1.0f, 100, 4.5f, true, true);
        Armadura corazaDelGuardián = new Armadura("Coraza del Guardián", "Una coraza impenetrable que ha protegido a generaciones de héroes", Rareza.Legendario, null, 150, 50, 30, 50, 15, 100, ParteArmadura.Central, true, true);
        Armadura grebasDeAgilidad = new Armadura("Grebas de Agilidad", "Greas ligeras que permiten al portador moverse con la velocidad del viento", Rareza.Raro, null, 70, 30, 20, 20, 30, 90, ParteArmadura.Piernas, true, true);
        Armadura yelmoDeSabiduría = new Armadura("Yelmo de Sabiduría", "Un yelmo antiguo que mejora la percepción y el juicio del portador", Rareza.Epico, null, 90, 40, 25, 30, 20, 85, ParteArmadura.Cabeza, true, true);
                
        inventarioPersonajePrincipal.AgregarObjeto(espadaDelAmanecer);
        inventarioPersonajePrincipal.AgregarObjeto(arcoDeSombra);
        inventarioPersonajePrincipal.AgregarObjeto(lanzaDeDestino);  
        inventarioPersonajePrincipal.AgregarObjeto(corazaDelGuardián);
        inventarioPersonajePrincipal.AgregarObjeto(grebasDeAgilidad);
        inventarioPersonajePrincipal.AgregarObjeto(yelmoDeSabiduría);
        inventarioPersonajePrincipal.AgregarMaterialBasico(TipoMaterialBasico.Madera, 30);
        inventarioPersonajePrincipal.AgregarMaterialBasico(TipoMaterialBasico.Cuero, 20);
        inventarioPersonajePrincipal.AgregarMaterialBasico(TipoMaterialBasico.Hierro, 10);
                
        //creamos un comerciante que emula al personaje principal y listamos su inventario
        Comerciante personajePrincipal = new Comerciante("Personaje principal", inventarioPersonajePrincipal);
        personajePrincipal.ListarInventario();

        //objetos disponibles para comprar
        Consumible pocionCurativa = new Consumible("Poción Curativa", "Una poción mágica que restaura la salud", Rareza.Raro, null, 10, 5, 50, "Restaura 50 de salud", 0, 100, true);
        Consumible elixirFuerza = new Consumible("Elixir de Fuerza", "Un poderoso brebaje que aumenta temporalmente la fuerza", Rareza.Epico, null, 20, 10, 100, "Aumenta la fuerza", 20, 100, true);
        Artefacto amuletoVida = new Artefacto("Amuleto de la Vida", "Un antiguo amuleto que otorga vitalidad adicional", Rareza.Legendario, null, 100, 50, 0, "Aumenta la salud máxima", true);
        Artefacto cristalSabiduria = new Artefacto("Cristal de Sabiduría", "Un cristal místico que ilumina la mente del portador", Rareza.Epico, null, 80, 40, 10, "Mejora la regeneración de maná", true);
        Artefacto piedraEterna = new Artefacto("Piedra Eterna", "Una piedra mágica que es fuente de energía inagotable", Rareza.Raro, null, 60, 30, 20, "Reduce el tiempo de recarga de habilidades", true);
        
        //comercio
        personajePrincipal.Comprar(pocionCurativa);
        personajePrincipal.Vender(espadaDelAmanecer);
        personajePrincipal.Comprar(pocionCurativa);
        personajePrincipal.Comprar(piedraEterna);
        personajePrincipal.Vender(espadaDelAmanecer);
        personajePrincipal.Vender(yelmoDeSabiduría);
        personajePrincipal.Comprar(elixirFuerza);
        personajePrincipal.Comprar(cristalSabiduria);
        personajePrincipal.Comprar(amuletoVida);
        personajePrincipal.ComprarMaterialBasico(TipoMaterialBasico.Madera, 10);
        personajePrincipal.VenderMaterialBasico(TipoMaterialBasico.Madera, 20);
        personajePrincipal.ComprarMaterialBasico(TipoMaterialBasico.Diamante, 5);
        personajePrincipal.VenderMaterialBasico(TipoMaterialBasico.Madera, 40);

        //listamos el inventario del personaje principal
        personajePrincipal.ListarInventario(); 
    }
}
