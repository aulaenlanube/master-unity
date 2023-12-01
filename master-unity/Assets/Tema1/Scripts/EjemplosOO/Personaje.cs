public abstract class Personaje
{
        private string nombre;
        private int vida;
        public Personaje(string nombre, int vida)
        {
            this.Nombre = nombre; // usa el setter
            this.Vida = vida;     // usa el setter
        }
        public string Nombre // getter y setter para nombre
        {
            get { return nombre; }
            set { nombre = value; } // 'value' en C# que representa el valor asignado
        }
        public int Vida //getter y setter para vida
        {
            get { return vida; }
            set { vida = value < 0 ? 0 : value; }  // validación: la vida no puede ser negativa
        }
        public virtual void RecibirGolpe(int n)
        {
            this.Vida -= n; // usa el setter con validación
        }

    public abstract void RealizarAccionEspecial();
}
