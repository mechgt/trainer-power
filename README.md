
# Trainer Power Track
Trainer Power plugin for SportTracks (Desktop). Used to estimate cycling power based on a stationary trainer session.

Create a power track for your indoor training sessions based off of a speed/distance data track.

This software was open-sourced after SportTracks desktop was discontinued, and all restrictions have been removed.

### Getting Started
2 Steps:
1) Associate a Trainer with SportTracks equipment (in other words... your trainer).
2) Select trainer activity, and click Edit > Create Power Track: (trainer name)

See below for details...

Go to Settings page and select your trainer from the trainers list.  Some trainer models have several options.  These may be for adjustable resistance models, regional models, or any number of other reasons, just pick the trainer most appropriate to you.  Use the power curve as a guideline to help you decide if your trainer has several listed.

Once you select your trainer from the top list, associate it with a piece of equipment defined in SportTracks and click the Add button (blue arrows).  That's enough to get you started.

![tp_settings](https://mechgt.com/st/images/tp_settings.png)

### Additional Notes
Trainer power curves are defined by a 3rd order polynomial as shown in the yellow highlight.  This equation defines the power curve shown by the green arrow.  You can modify any of the trainer definitions necessary... change names, constants, etc.  Just click the edit button.

Trainer not listed?  Send me a request and I'll be sure to add it to the next release... in the meantime, you can add it yourself by clicking the Add button to add a new trainer to the list (orange arrows).

### Create Power Track

Now that you've got your trainer setup, and associated with your SportTracks equipment, select an activity - or a whole bunch of them from Reports View! (hint hint) - and select Edit > Power > Create Power Track: (trainer model) as shown in the screenshot below.  That's it!

NOTE:  Your activity must have 2 things: a speed/distance track, and a piece of equipment associated with a trainer.  If it doesn't have a speed/distance track, a power track cannot be created.

![tp_action](https://mechgt.com/st/images/tp_action.png)

Below is a sample output from an indoor training video...

![tp_samplechart](https://mechgt.com/st/images/tp_samplechart.png)

### Sample Data

Below is an example file created by a Trainer Power user comparing the Trainer Power plugin output to a PowerTap cycling power meter.  Click the image to view full size:

![tp_sample](https://mechgt.com/st/images/tp_sample.png)

### Live Power Display
When installed with Live Recording and HRV plugin (by OldManBiking), Trainer Power can supply live power values for real time power values while riding on your trainer.  Configure your trainer in Trainer Power, and Trainer Power will supply power values to Live Recording plugin.  See Live Recording and HRV plugin page for more details.
NOTE: Live Recording plugin not developed by mechgt.

### Languages Fully Supported
English, Deutsch, español, français, italiano, Nederlands, norsk, polski, português, svenska, 中文(台灣)
Many others will be partially supported.