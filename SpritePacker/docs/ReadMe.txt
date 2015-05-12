The project takes in singe/multiple image files of various formats (only bmp, jpeg, gif, and png officially supported but feel free to try any valid image format) and outputs a single bmp/png (user choice) of all the taken files as well as an xml file that has an an element for the whole sheet with attribute of filename, and element of each sprite with attribute of each sprite name, location on canvas, and size.

Case to test:	Import valid image file
Instructions:	Click "Import File..." button, click on "image.bmp", click open
Expectation:	File is accepted and preview is displayed
Result:		Pass

Case to test:	Add image to canvas
Instructions:	Click "Import File..." button, click on "image.bmp", click open, click "Add To Canvas"
Expectation:	File is accepted, preview is displayed, image is added to canvas
Result:		Pass

Case to test:	Add multiple images to canvas
Instructions:	Click "Import and Add Multiple..." button, press CTRL + A, click open, click "Add To Canvas"
Expectation:	Valid files are accepted, invalid files are rejected, valid files added to canvas
Result:		Pass

Case to test:	Save image to directory
Instructions:	Click "Import File..." button, click on "image.bmp", click open, click "Add To Canvas", click "Save As...", enter "test" into the File name box, click "Save"
Expectation:	File is accepted, preview is displayed, image is added to canvas, test.bmp and test.xml are created in chosen directory
Result:		Pass

Case to test:	Import a non-image file
Instructions:	Click "Import File..." button, click on "nonimage.bat", click open
Expectation:	File is rejected as non-image file
Result:		Pass

Case to test:	Import a false image file (nonimage.bat renamed to nonimage.bmp)
Instructions:	Click "Import File..." button, click on "nonimage.bmp", click open
Expectation:	File is rejected as invalid image file
Result:		Pass

Case to test:	Import a corrupt image file
Instructions:	Click "Import File..." button, click on "corrupt.bmp", click open
Expectation:	File is rejected as corrupt image file
Result:		Pass

Case to test:	Import an empty file
Instructions:	Click "Import File..." button, click on "file", click open
Expectation:	File is rejected as NULL
Result:		Pass

Case to test:	Add empty preview image to canvas
Instructions:	Click "Add To Canvas" with no image in the preview
Expectation:	Added image is rejected as NULL
Result:		Pass
