# SCP-008

A simple SCP SL [Synapse](https://github.com/SynapseSL/Synapse/) Plugin to bring SCP-008 into the game.  
In addition to SCP-008, this plugin shall also offer some SCP-049 utilities.

---
### Configs

For base-game item ids, checkout [DefaultItemIDs](DefaultItemIDs.md)

Default config:

```yaml
[SCP-008]
{
scp049Configs:
# Chance to not one hit K.O. as SCP 049
  nonOHKChance: 0
  # Amount of damage in % of target's max-life when SCP 049 does not one hit K.O.
  nonOHKDamage: 0
  # Chance of 049 infecting the target on non-one hit K.O.?
  infectChanceOnNonOHK: 0
scp008Configs:
# Roles which can spread 008
  infectingRoles:
  - 10
  # Toggle % damage and the amount for 008's damage over time tick
  damagerPerTickPercentage:
    enabled: false
    amount: 0
  # Toggle static damage and the amount for 008's damage over time tick
  damagerPerTickStatic:
    enabled: false
    amount: 0
  # 008 damage per tick
  damagerOverTimeInterval: 2.5
  # Healing items which remove SCP 008
  infectionHealItems:
  - 17
}
```
