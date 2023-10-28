import sys
import pytesseract
from PIL import Image

def imageToText(imagePath):
    image = Image.open(imagePath)

    extractedText = pytesseract.image_to_string(image)

    return extractedText

def main():
    if len(sys.argv) != 2:
        print("Usage: python ImageToText.py <image_path>")
        return

    # ask the user to select the picture path they want to convert
    imagePath = sys.argv[1]

    # stores all the text analyzed from the picture in a variable
    extractedText = imageToText(imagePath)
    print(extractedText)

# guard code
if __name__ == "__main__":
    main()

