
## Description: 
ORBIT is a prototype user interface for the proposed Gateway Lunar station. This project was created in response to the eXploration Systems and Habitation (X-Hab) Academic Innovation Challenge by the National Space Grant Foundation. The X-Hab challenge called for creating a user interface that would operate completely autonomously as well as share autonomy with ground and crew members. The project was not chosen to receive grant funding, however development proceeded as a senior capstone project at Utah Valley University. The students developing the project consisted of three senior UX/UI design majors and three Software Engineering majors. This team collaborated on the research, design, and development of ORBIT.  

During initial research and design discussions, a significant amount of loose wiring and laptops scattered throughout the International Space Station (ISS) was noticed. This looked to present hazards to equipment and crew members should the bump into a computer or become caught in a wire while moving about in a zero-gravity environment. This problem would be more significant aboard the smaller Lunar Gateway. The viability of using an augmented-reality headset for Gateway system access instead of multiple laptops or static displays came up during discussions of what display size and system ecosystem we would develop for. 

Use of a HoloLens 2 augmented-reality device for the user interface was initially chosen as it presented several benefits. The headset could be seen through when the system was off or low opacity allowing continued awareness of surroundings, requires little space and access to the Gateway’s systems could obtained anywhere the crew member was. Functionality could be also extended to space walk and surface missions though a helmet mounted device. Mission check sheets and suit system information could also be displayed for the crew member. Use of gestures would minimize glove dexterity issues while navigating the interface. HoloLens is also used by Lockheed Martin for their MAIA system which is an artificial intelligence assistant to help crew troubleshoot module problems during extended missions. One device offered a variety of uses and applications. 

While initial design and development were for the HoloLens, Utah Valley University was unable to obtain a device for student use. Development then shifted to the Oculus Quest with the idea of simulating an augment reality interface within a virtual reality space. The Quest was a promising substitute as Oculus planned to release hand tracking and gesture support. 

The system was created in two parts. The first is set of library files that represent the core functions of the system. There is code to support using Entity Framework to write to and from a database within the ORBIT library but is not being used in the current prototype due to compatibility issues with Unity. The ORBIT library contains the code used to simulate the systems. The library files were imported as Orbit.dll once built.

Unity was chosen to implement the UI portion of ORBIT as it was optimized in collaboration with Oculus to support development for the Quest. Oculus Integration contains many ready to use components that needed little configuration to work. Unity allowed for rapid testing to be done without having to take time to load the program into the Quest after every iteration. The drag and drop nature of Unity also made for easy adjustments and additions to UI layouts. 


## UI Functionality
### Toggle Buttons

- #### Auto/Manual

Auto: System will change states or settings autonomously based on simulated sensor data and preset operational ranges. No crew interaction is required. 

Manual: System will continue to operate in current state regardless of sensor data. This setting assumes a crew member will be manually changing the system states when operation outside of preset parameters is required or the system needs to be in a ‘Standby’ state for maintenance. 

- #### Crewed/Un-crewed 

Crewed: Changes operational parameters to those supporting human habitation. Temperature operational values increase, lights will turn on and systems that were in a ‘Standby’ state will change to ‘Processing’ or ‘Active’ such as the oxygen generator and carbon dioxide remover. 

Un-crewed: Operational parameters will change to those that put the station into an inactive state. Systems non-essential when the station is uninhabited will change to a ‘Standby’ state to conserve power and reduce wear and tear on components. This includes lowering the temperature, not generating oxygen or processing water. 

- #### Process/Standby

This selection is unavailable and grayed out when the system in in ‘Auto’ mode. 

Process: Active state. There is a visual component on each system that will move to show that the system is active. 

Standby: Inactive state. A lack of component movement is a quick cue to show that the system is in a ‘Standby’ state. 

Manual mode is selected, un-graying the allowing interaction with the ‘Process’/’Standby’ toggle
 
<img src="UVU_ORBIT_Unity/images/ungrayed.png" width=75% height=75% alt="Screenshot of ungrayed process/standby toggle">

## Station Systems Operation
- #### Cabin/Atmosphere

Cabin represents the current conditions within the station. Temperature, humidity, carbon dioxide, oxygen are monitored here. Each cabin module can be accessed by clicking on its name. Light levels can be changed in each module. Each module except Habitation and Multipurpose has a pod in the scene whose window will change color and intensity when the light level selection is changed. Blue buttons represent daytime color temperature while the orange buttons represent evening or nighttime color temperatures. This helps to regulate day/night cycles for the crew while inhabiting the station. Noise levels are also monitored to ensure the large amount of equipment and fan noise does not cause harm to the crew. 

  
<img src="UVU_ORBIT_Unity/images/cabin1.png" width=50% height=50% alt="Screenshot of Cabin system">

- #### Power

 - Power is generated from solar cells that also charge the onboard batteries. Optimal charging is maintained as long as possible by rotating the solar panels towards the sun when the station is sun facing. The batteries provide power to the station when unable to obtain enough light (eclipse state). While charging, the solar panels will turn blue and the battery will light up from the negative side towards the positive side. The opposite will occur when simulating battery usage while the station is in an eclipse state. By selecting Battery, solar charging will be disabled.  

<img src="UVU_ORBIT_Unity/images/power.png" width=75% height=75% alt="Screenshot of oxygen system"> 

- #### Oxygen

 - Oxygen is generated through the electrolysis of water. How much oxygen is being produced at any one time is controlled by activating or deactivating an electrolysis ‘cell’. Water that has been fully treated by the water system is used for electrolysis after any bubbles have been removed. Oxygen is vented to the cabin from the anode (positive terminal) while hydrogen from the cathode (negative terminal) is collected and stored for use in water generation. A sensor checks for hydrogen leaks within the system compartment. There are total of 10 electrolysis cells represented. The number of active cells can be changed while the system is in an ‘Auto’ but will not be remain constant. Use of ‘Manual’ mode will allow crew to select a desired number of active cells which the system will hold. Selecting ‘Standby’ will inactivate all cells. 
 
 <img src="UVU_ORBIT_Unity/images/oxygen.png" width=75% height=75% alt="some_text">

- #### Carbon Dioxide

 - There are two identical sets of components that make up the carbon dioxide scrubber, one will be absorbing carbon dioxide from cabin air while the other will be releasing the absorbed carbon dioxide to storage. A mineral called zeolite is formed into pellets which makes up the renewable carbon dioxide ‘sponge’. Air from the cabin is blown into the absorbing side of the system over the pellets, carbon dioxide is removed, and the air is released back into the cabin. The pellets are regenerated with heat, the carbon dioxide is captured and stored for use in the Sabatier reaction process. When the exhaust carbon dioxide level exceeds a limit, the sides switch and the process begins again. When placed in ‘Manual’, ‘Standby’ will turn off heaters and fan ‘Processing’ will resume function. ‘Bed 1’ and ‘Bed 2’ pertain to the bed that is absorbing carbon dioxide and can be toggled by the crew. The same bed remain in use until changed by the crew when system is in ‘Manual’ mode. : 
 
 <img src="UVU_ORBIT_Unity/images/co2.png" width=75% height=75% alt="Screenshot of carbon dioxide system">
 
- #### Sabatier

 - This system is named for the reaction process discovered by French chemists Paul Sabatier and Jean-Baptiste Senderens. The Sabatier system generates water by reacting hydrogen and carbon dioxide, producing methane as a waste product. This reaction requires temperatures of over 300C to occur, though once started the reaction is exothermic and maintains itself. This system utilizes waste products from the carbon dioxide scrubber and oxygen generator. Research is underway to find a way to best utilize the methane as propellant once additional oxygen can be generated from the lunar or Mars surface. When placed in ‘Manual’ mode, the only options are to process or be in an inactive state. 

<img src="UVU_ORBIT_Unity/images/sabatier.png" width=75% height=75% alt="Screenshot of sabatier system">
 
- #### Water

 - Urine is collected and distilled separately from other wastewater sources. Concentrated distillate material is stored separately in the brine tank. This tank requires emptying periodically. The UI simulates this as happening automatically. Once distilled, the urine water is mixed with the other ‘dirty’ water which is then ultra-filtered, air bubbles removed, then heat treated with a reagent to remove any other contaminants. The fully treated water is then cooled and placed into a storage container and is ready to be used for drinking or moved to other storage units. While sounding unsanitary, the treated water has no bad taste and is of better quality than many municipal water systems. Crew members can select to have either or both the distiller and water processor run in manual mode. Currently the only options are an active ‘Processing’ state or inactive ‘Standby’ state. 
 
 <img src="UVU_ORBIT_Unity/images/water.png" width=75% height=75% alt="Screenshot of water system">

- #### Thermal

 - The Thermal System represents two separate, yet linked systems. The internal cooling system circulates water through two separate loops withing the station itself. One loop operates at just above freezing (2-10C) and is used to remove heat and humidity from the atmosphere to maintain a comfortable environment for the crew. The medium temperature loop operates between 12-22C and is used to cool equipment. Heat transfer occurs through heat transfer plates which are equipped with fins that mesh with fins on the equipment. The external cooling system is where heat is dissipated into space through cooling radiators. Ammonia is used in the external loop due to its very low freezing point. The radiators are adjusted to have the thin edge face the sun to prevent the ammonia from heating. 

 - The interface between the two systems is the Internal/External Heat Exchanger. Fluid from both the internal and external systems circulates next to each other exchanging heat through adjacent tubing networks, but never mixes. This ensures there is no toxic ammonia within the station should a leak develop. A mixing valve on the external system ensures that the ammonia that reaches this heat exchanger is never cold enough to freeze the water while another set of mixing valves in the internal system keeps each loop at its optimal temperature. This is done by controlling the mix of ‘hot’ and ‘cold’ coolants in each system at the Internal/External Heat Exchanger. Manual mode and an active or inactive state can be selected independently for each system. The corresponding pump will show as ‘OFF’ when in ‘Standby’, and hold if selected while in manual mode. 
 
<img src="UVU_ORBIT_Unity/images/thermal.png" width=75% height=75% alt="Screenshot of thermal system">


## Bottom Menu

- Toggle controls for the Checklist, Systems panel, Communication panel and Suit values on the bottom are closed. Each can be opened or closed independently of the others. The different light color and intensities of the pods can be seen with the systems panel closed. 
 
<img src="UVU_ORBIT_Unity/images/bottommenu.png" width=75% height=75% alt="Screenshot of all panels closed"> 
   

- View with only the Systems Panel open. 
     
     <img src="UVU_ORBIT_Unity/images/systemspanel.png" width=75% height=75% alt="Screenshot of systems panel open"> 
 
 
## Task Panel
- Selecting the first item on the checklist will open up a dropdown of subtasks. These can be checked or un-checked individually by clicking on their boxes. Clicking on the parent box will check or un-check all of the subtasks. The checked values will persist if the subtask lists or checklist screen in closed and reopened. 
 

 - Checklist is open with two subtasks completed. 
<img src="UVU_ORBIT_Unity/images/2subtasks.png" width=75% height=75% alt="Screenshot of two subtasks completed"> 

