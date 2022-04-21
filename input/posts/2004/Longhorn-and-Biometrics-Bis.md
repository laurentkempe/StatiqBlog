---
title: "Longhorn and Biometrics, Bis"
permalink: /2004/03/17/Longhorn-and-Biometrics-Bis/
date: 3/17/2004 6:31:00 AM
updated: 3/17/2004 6:31:00 AM
disqusIdentifier: 20040317063100
alias:
 - /post/Longhorn-and-Biometrics-Bis.aspx/index.html
---
I checked a bit what I can find in Longhorn concerning that Biometric stuff. And I found that it is [BAPI](http://www.iosoftware.com/pages/Products/Biometric%20API/index.asp): bapi.dll, . That specification was bought some long time ago from i/o software and I remember that it was planned to have this integrated in Windows XP. I think that Microsoft did not included it because the biometric technology was not ready at that time.

I know a bit more about BioAPI than BAPI, but at the end I think it works almost the same. There is also a <strong><em>Kernel Biometric Service Provider</em></strong>: kbsp.dll and a tool called biotool.exe permiting to enroll a new biometric credential:
<!-- more -->

BIOTOOL <command> <method> <source>

  commands:

      ENROLL  Enroll a new biometric credential.

  methods:

      FP      Fingerprint

  sources (specific to each method):

      RT    Right Thumb (FP)<br>      RI    Right Index (FP)<br>      RM    Right Middle (FP)<br>      RR    Right Ring (FP)<br>      RP    Right Pinky (FP)<br>      LT    Left Thumb (FP)<br>      LI    Left Index (FP)<br>      LM    Left Middle (FP)<br>      LR    Left Ring (FP)<br>      LP    Left Pinky (FP)
