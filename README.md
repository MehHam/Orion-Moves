# Orion-Moves VR project

 * Synopsis : Help Orion the hunter creating your own moves nebulae. Stars intensity depend on the intensity of each movement. The nebulae is distibuted over a Cube.
 
 
![alt tag](https://github.com/MehHam/Orion-Moves/blob/master/Images/Cube.png)
 
 * Pedagogical principle : The major aspect is a rigourous ludo-pedagogical représentation of motion and movement. As shows the figure below, A gesture contains a set of data sent  using a **Movuino** or **Smartphone** UDP streamer (see https://github.com/MehHam/Moves-Remote). A set of data is given by  "Acc\t" + Input.acceleration.ToString() +"\t"+ "Gyro\t" + gyro.gravity.ToString()" and locating the position of a star in the "Orion Nebulae" depends on the Gyro coordinates (locates the intensity /color of the gesture star), its diameter is correlated to the acceleration vector magnitude. Finally, The star transparency depends on the weighted occurences at that position.
  
 ![alt tag](https://github.com/MehHam/Orion-Moves/blob/master/Images/Principle.png)
 
# VR Options  : 
- Resolution : By clicking on the Resolution Option (blue ) on can change the number of accessible positions in the Orion Nebulae.
- Point Of View : By clicking on the Position Option (pink ) on can change the observation position in the Orion Nebulae.

![alt tag](https://github.com/MehHam/Orion-Moves/blob/master/Images/inside.png)

- Static Vector : By Clicking the Static option (Green), on can observe the Nebulae in its totality.
- Remote : By clicking on the Remote Option (yellow) for switching to remote data capture.
- Infos : The White Option Offers a couple of informations about the mode, UDP port, etc...

![alt tag](https://github.com/MehHam/Orion-Moves/blob/master/Images/random.png)






