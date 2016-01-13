:game_die: roll
=======

- Dice rolling random number generator for Unity
- Only single file less than 200 lines of code.
- No external assets required.
- Easy to integrate.
- roll is MIT licensed.

### Example Usage

```c#
int v1 = Roll.d6.Dice();

Roll r1 = new Roll("d10");
int v2 = r1.Dice();

Roll r2 = new Roll("2d6+3");
int v3 = r2.Dice();
```

### Other API

- There is a second constructor `Roll(int count, int face, int add)` to skip the notation, and enter the rules directly.
- You can inherit from the `Roll` class, and override the `int Roll.Random()` function to replace the RNG with your own.
- There are provided `Roll.d6`, `Roll.d10`, `Roll.d20` and `Roll.d100` rolls already provided.

## Releases
- v1.0.0 (13/01/2016)
  - Initial Release