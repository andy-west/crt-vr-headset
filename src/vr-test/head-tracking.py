#!/usr/bin/python3

import atexit
import sys
import threading

from flask import Flask
from flask_cors import CORS, cross_origin

ahrs_data = ""
data_lock = threading.Lock()
background_thread = threading.Thread()

app = Flask(__name__)
CORS(app)

def exit_handler():
    global background_thread
    background_thread.cancel()

def read_tracking_data():
    global ahrs_data
    global background_thread

    try:
        line = input()

        with data_lock:
            ahrs_data = line
        
        background_thread = threading.Timer(0.01, read_tracking_data)
        background_thread.start()
    except EOFError:
        pass

read_tracking_data()
atexit.register(exit_handler)

# ------------------------------------------------

@app.route("/")
def index():
    return "Head tracking API."

@app.route("/trackingdata")
@cross_origin()
def tracking_data():
    values = "0.000, 0.000, 0.000, 0.000"

    if not ahrs_data.startswith("Error"):
        lineValues = ahrs_data.split()
        values = "{0:.3f},{1:.3f},{2:.3f},{3:.3f}".format(float(lineValues[0]), float(lineValues[1]), float(lineValues[2]), float(lineValues[3]))

    return values

if __name__ == "__main__":
    app.run(debug=True, host="0.0.0.0")
