
public class Program
{
    public void TakeDamage(float amount, Unit whoDealt)
    {
        ref readonly var s = ref GetUnitStatsBillboard();

        float finalAmount = amount - s.HealthStats.Armor;
        if (finalAmount <= 0) return;

        HealthState.CurrentHealth -= finalAmount;

        TakeDamageEvent?.Invoke(new DamageInfo()
        {
            Amount = finalAmount,
            Instigator = whoDealt,
        });
        OnTakeDamageEvent?.Invoke();

        if (gameObject != null)
        {
            if (whoDealt != null)
                Debug.Log(gameObject.name + "Dam: " + amount + ". Cur: " + HealthState.CurrentHealth + ". Source: " + whoDealt.gameObject.name + ", Owner of source: " + whoDealt.Owner);
            else
                Debug.Log(gameObject.name + "Dam: " + amount + ". Cur: " + HealthState.CurrentHealth + ". Dealt by: null");
        }

        if (HealthState.CurrentHealth <= 0)
        {
            KilledEvent?.Invoke(new KillInfo()
            {
                Killer = whoDealt,
            });
            Die();
        }
    }