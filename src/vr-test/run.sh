#! /bin/bash
cd /home/pi/vr-test
minimu9-ahrs --mode compass-only --output quaternion | ./head-tracking.py &
BGPID1=$!
devilspie &
BGPID2=$!
chromium-browser --allow-file-access-from-files --disable-web-security vr-test.html
kill $BGPID2
kill $BGPID1
kill %1
