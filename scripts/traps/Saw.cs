using Godot;

    public partial class Saw : Area2D
    {
        
        public void _on_body_entered(Node2D body)
        {
            if (body.GetType() == typeof(MainCharacter))
                ((MainCharacter)body).Kill();
        }
    }