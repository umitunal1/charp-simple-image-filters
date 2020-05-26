# WPF Simple Image Processor
This is a project that applying some filters on image. There are 3 filters: Red Filter, Gray Filter, and Average Filter 

### Red Filter
Suppressing red channel of each pixel.
![alt text](http://umitunal.org/wp-content/github/sip1.PNG)

### Gray Filter
There are several grayscale filters. This is the simplest one that sums each channels(r,g,b) of each pixel and then gets average of this sum.\
So each pixel is : OriginalPixel(r+g+b)/3
![alt text](http://umitunal.org/wp-content/github/sip2.PNG)

### Average Filter
The average value of each pixel and tbe 4 pixels left, right, top and bottom.\
So, each pixel is = (original value+ left+right+top+bottom) / 5 \
If the pixel lies at the border its value remains the same.
![alt text](http://umitunal.org/wp-content/github/sip3.PNG)
