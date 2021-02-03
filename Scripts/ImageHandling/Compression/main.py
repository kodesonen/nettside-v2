from PIL import Image
import PIL
import os
import glob
import sys
import configparser

class Compression:

    def __init__(self):
        config = configparser.ConfigParser()
        config.read('settings.ini')

        self.pathRoot = config.get('settings','root_path')
        self.folders = config.get('settings', 'folders').split()
        self.formats = config.get('settings', 'formats').split()

    def GetFolders(self):
        return self.folders

    def GetValidFormats(self):
        return self.formats

    def ResizeImg(self, basewidth, img):
        wpercent = (basewidth / float(img.size[0]))
        hsize = int((float(img.size[1]) * float(wpercent)))

        img = img.resize((basewidth, hsize), Image.ANTIALIAS)
        return img

    def Compressing(self, specifyImg, directoryPath):
        height = width = basewidth = 0

        config = configparser.ConfigParser()
        config.read('settings.ini')

        path = self.pathRoot + directoryPath + "/"
        quality = int(config.get(directoryPath, 'quality'))

        if config.has_option(directoryPath, 'custom_size'):
            width = int(config.get(directoryPath, 'width'))
            height = int(config.get(directoryPath, 'height'))
        else:
            basewidth = int(config.get(directoryPath, 'basewidth'))

        os.chdir(path)
        files = os.listdir()

        if not specifyImg:
            images = [file for file in files if file.endswith(tuple(self.formats))]

            for image in images:
                img = Image.open(image)

                if basewidth > 0:
                    img = self.ResizeImg(basewidth, img)
                elif width > 0 and height > 0:
                    img = img.resize((width, height), Image.ANTIALIAS)

                img.save(image, optimize = True, quality = quality)

        else:
            image = Image.open(specifyImg)
            
            if basewidth > 0:
                image = self.ResizeImg(basewidth, image)
            elif width > 0 and height > 0:
                image = image.resize((width, height), Image.ANTIALIAS)
            
            image.save(specifyImg, optimize = True, quality = quality)


start = Compression()
folders = start.GetFolders()

if sys.argv[0] == "build":
    for folder in folders:
        start.Compressing("", folder)
elif sys.argv[0] == "build" and sys.argv[1] == "all":
    print("Build all")
else:
    start.Compressing(sys.argv[0], "upload")
