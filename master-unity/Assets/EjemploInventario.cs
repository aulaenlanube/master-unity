using UnityEngine;

public class EjemploInventario : MonoBehaviour
{
    void Start()
    {
        Inventario inventario = new Inventario();

        Arma espadaAcero = new Arma("Espada de Acero", "Espada con una gran durabilidad pero muy pesada", Rareza.Comun, null, 10, 5, 0, NivelArma.Inicial, 15.0f, 1.0f, 100, 1.5f);
        Arma espadaAceroEpica = new Arma("Espada de Acero", "Espada con una gran durabilidad pero muy pesada", Rareza.Epico, null, 100, 0, 0, NivelArma.Elite, 50.0f, 3.0f, 300, 3f);
        Arma arcoLargo = new Arma("Arco Largo", "Arco de gran tamaño que proporciona un gran alcance", Rareza.Raro, null, 15, 10, 0, NivelArma.Avanzado, 12.0f, 0.8f, 80, 5.0f);

        Armadura armaduraCuero = new Armadura("Armadura de Cuero", "Armadura con una resistencia básica pero muy ligera", Rareza.Comun, null, 8, 3, 0, 10, 5, 100, ParteArmadura.Central);

        Consumible pocionSalud = new Consumible("Poción de Salud", "Poción que permite recuperar puntos de salud", Rareza.Comun, null, 2, 1, 0, "Restaurar Salud", 10f, 100f);
        Consumible pocionSaludRara = new Consumible("Poción de Salud", "Poción que permite recuperar puntos de salud", Rareza.Raro, null, 4, 0, 0, "Restaurar Salud", 20f, 100f);
        Consumible pociconFuerza = new Consumible("Poción de Fuerza", "Poción que aumenta temporalmente la fuerza", Rareza.Raro, null, 5, 2, 0, "Aumentar Fuerza", 10f, 100f);

        ObjetoEspecial amuletoMisterioso = new ObjetoEspecial("Amuleto Misterioso", "Amuleto de plata con mucho detalle, forma una estrella de 5 puntas", Rareza.Epico, null, 50, 25, 5, "Aumenta la suerte");


        // añadir objetos
        inventario.AgregarObjeto(espadaAcero);
        inventario.AgregarObjeto(espadaAceroEpica);
        inventario.AgregarObjeto(arcoLargo);
        inventario.AgregarObjeto(armaduraCuero);
        inventario.AgregarObjeto(pocionSalud);
        inventario.AgregarObjeto(pocionSalud);
        inventario.AgregarObjeto(pocionSaludRara);
        inventario.AgregarObjeto(pociconFuerza);
        inventario.AgregarObjeto(amuletoMisterioso);
        Debug.Log("INVENTARIO ANTES:");
        inventario.MostrarInventario();


        // eliminar objetos
        inventario.EliminarObjeto(espadaAcero);
        inventario.EliminarObjeto(pocionSalud);
        inventario.EliminarObjeto(pocionSalud);
        inventario.EliminarObjeto(pocionSalud);
        // mostrar el inventario después de eliminar objetos
        Debug.Log("INVENTARIO DESPUÉS:");
        inventario.MostrarInventarioPorValor();
        Debug.Log("OBJETOS ÉPICOS:");
        inventario.MostrarInventarioPorRareza(Rareza.Epico);
    }
}
