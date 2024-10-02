
// Abstract class for other classes to extend from
public abstract class Task
{
    // Classes extending from Task must include an override method for 'Update' 
    public abstract void Update();

    // Classes extending from Task can optionally override the 'Activate', 'Suspend', and 'Complete' methods
    public virtual void Activate() {}
    public virtual void Suspend() {}
    public virtual void Complete() {}
}
