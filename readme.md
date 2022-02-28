<!--
Heading
Italics Strong Strikethrough
Blogquote
Links Images
UL and OL
Code blocks
Task List-->

# Heightmap Generation

Generates heightmap texture images using **Perlin Noise**

## Perlin Noise
> Perlin noise is a procedural texture primitive, a type of gradient noise used by visual effects artists to increase the appearance of realism in computer graphics.

Perlin Noise is one of the techniques used to model realistic terrains. It creates more real-life like mountains and valleys. It is generated using random noise.

### Random Noise
We compute random noise by generating a random value from 0 to 1 and assigning the texture a color based on this value

```cs
int shade = (int)(Random.NextDouble() * 255);
```

Sample Random Noise

![Random Noise Texture](https://i.ibb.co/PTYPYCp/random-noise.png)

### Octaves
For generating perlin noise we have to generate octaves of the random noise. We select pixels at intervals and linearly interpolate remaining pixels to make it smooth.

#### Wavelength
The intervals at which we select pixels is dependent on *wavelength* of the Perlin noise function. The best value can be differnt in differnt use cases. Using ```wavlength = 2.0f``` is a good starting point for our project.

We can find the interpolation interval by using formula
```cs
int stide = (int)(Math.Pow(2.0f, octave_number));
```

We find perlin octaves for different values of octave_number and then combine the octaves to generate the final heightmap.

For the above random noise if we generate octaves for octave_number ```1 to 7``` we get

Octave #1

![Octave #1](https://i.ibb.co/kSTrwvV/perlin-octave1.png)

Octave #2

![Octave #2](https://i.ibb.co/y4yVJ0H/perlin-octave2.png)

Octave #3

![Octave #3](https://i.ibb.co/ns2ZNcj/perlin-octave3.png)

Octave #4

![Octave #4](https://i.ibb.co/6P9tWXm/perlin-octave4.png)

Octave #5

![Octave #5](https://i.ibb.co/2h54hxm/perlin-octave5.png)

Octave #6

![Octave #6](https://i.ibb.co/PgzYrn1/perlin-octave6.png)

Octave #7

![Octave #7](https://i.ibb.co/txBVGgX/perlin-octave7.png)


### Amplitude
To generate perlin noise we combine the octaves generated to a final image texture. But if just average out the values in different octaves we end up with a very messy looking noise very similar to the random noise we generated earlier.

So to avoid this we use weighted average. We use amplitude to decide how much weight each octave should carry. This amplitude will define how much the octave influences the final image output. Just like wavelength, the is no best value of amplitude and should be found by experimentation of our particular use case. But ```amplitude = 0.5f``` is a good value to start messing with perlin noise so we will use this a default value.

We the compute amplitude for each octave using formula
```cs
float amplitude = Math.Pow(0.5f, octave_number);
```

We then take the weighted averages and combine the result to produce the final perlin noise texture. If we have generated ```k``` octaves we find weighted average using

```cs
float totalAmplitude = 0f;
for(int i = 0; i < k; i++)
{
    float amplitude = Math.Pow(0.5f, octave_number);
    totalAmplitude += amplitude;
    result[x, y] += octaves[x, y] * amplitude;
}
result /= totalAmpluitude;
```

The reulting texture should look something like below

Sample Perlin Noise

![Perlin Noise](https://i.ibb.co/HdHsMqs/heightmap.png)

---

Importing this into Unity and applying the heightmap to a terrain we get a procedural terrain with nature like mountains and valleys

Heightmap applied to terrain

![Visual Heightmap](https://i.ibb.co/0JRJjCV/unity-terrain-demo-v1.png)

Ofcouse this is not the most natural looking terrain and far from what real nature's production looks like. So I will keep exploring new techniques to make the heightmap more natural.

Some topics I plan on exploring
* [ ] Erosion simulation
* [ ] Generation using geology
