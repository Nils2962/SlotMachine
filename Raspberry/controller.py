import RPi.GPIO as GPIO
import time

GPIO.setmode(GPIO.BCM)

GPIO.setup(17, GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #Right
GPIO.setup(27, GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #RightCenter
GPIO.setup(22, GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #Center
GPIO.setup(26, GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #LeftCenter
GPIO.setup(6, GPIO.IN, pull_up_down=GPIO.PUD_DOWN) #Left

pressed = False

while True:
    if GPIO.input(17) == 1 and pressed == False:
        print('Right')
        pressed = True
    if GPIO.input(27) == 1 and pressed == False:
        print('RightCenter')
        pressed = True
    if GPIO.input(22) == 1 and pressed == False:
        print('Center')
        pressed = True
    if GPIO.input(26) == 1 and pressed == False:
        print('LeftCenter')
        pressed = True
    if GPIO.input(6) == 1 and pressed == False:
        print('Left')
        pressed = True

    if GPIO.input(17) == 0 and GPIO.input(27) == 0 and GPIO.input(22) == 0 and GPIO.input(26) == 0 and GPIO.input(6) == 0:
         pressed = False