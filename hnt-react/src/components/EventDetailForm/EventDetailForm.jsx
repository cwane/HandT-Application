import React, { useRef, useState } from 'react'
import { useForm as useHookForm } from 'react-hook-form'
import { FormProvider } from 'react-hook-form';
import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';
import { Input } from '../Input/Input';

const EventDetailForm = () => {
    // file drag and drop handler section
    const [mediaFiles, setMediaFiles] = useState([]);
    const [isDragging, setIsDragging] = useState(false);
    const fileInputRef = useRef(null);

    function selectFiles() {
        fileInputRef.current.click();
    }

    function onFileSelect(event) {
        const files = event.target.files;
        
        if(files.length === 0) return;

        for( let i = 0 ; i < files.length ; i++ ) {
            if(files[i].type.split('/')[0] !== 'image' && files[i].type.split('/')[0] !== 'video') continue;
            if (!mediaFiles.some((mediaFile) => mediaFile.name === files[i].name)) {
                setMediaFiles((prevMediaFiles) => [
                    ...prevMediaFiles,
                    {
                        type: files[i].type.split('/')[0],
                        name: files[i].name,
                        url: URL.createObjectURL(files[i])
                    }
                ])
            }
        }
    }

    function removeMediaFile(index) {
        setMediaFiles((prevMediaFiles) => 
            prevMediaFiles.filter((_, i) => i !== index)
        )
    }

    function onDragOver(event) {
        event.preventDefault();
        setIsDragging(true);
        event.dataTransfer.dropEffect = "copy";
    }

    function onDragLeave(event) {
        event.preventDefault();
        setIsDragging(false);
    }

    function onDrop(event) {
        event.preventDefault();
        setIsDragging(false);
        const files = event.dataTransfer.files;

        if(files.length === 0) return;

        for( let i = 0 ; i < files.length ; i++ ) {
            if(files[i].type.split('/')[0] !== 'image' && files[i].type.split('/')[0] !== 'video') continue;
            if (!mediaFiles.some((mediaFile) => mediaFile.name === files[i].name)) {
                setMediaFiles((prevMediaFiles) => [
                    ...prevMediaFiles,
                    {
                        type: files[i].type.split('/')[0],
                        name: files[i].name,
                        url: URL.createObjectURL(files[i])
                    }
                ])
            }
        }
    }

    const methods = useHookForm();

  return (
    <div>
        <FormProvider {...methods}>
            <form onSubmit={e => e.preventDefault()} noValidate>
                <section>
                    <Input
                        name="eventname"
                        label="Event Name"
                        type="text"
                        id="eventname"
                        placeholder="Annapurna Base Camp"
                        validation={{
                            required: {
                                value: true,
                                message: 'event title field is required' 
                            },
                            maxLength: {
                                value: 60,
                                message: 'event title must be less than 60 characters'
                            }
                        }}
                    />

                    <Input
                        name="eventcategory"
                        label="Event Category"
                        type="text"
                        id="eventcategory"
                        placeholder="Event Category"
                        validation={{
                            required: {
                                value: true,
                                message: 'event title field is required' 
                            }
                        }}
                    />

                    {/* upload images and videos via here */}
                    <article>
                        <h1>Upload Event Banner or Video</h1>

                        <div className='drag-area' onDragOver={onDragOver} onDragLeave={onDragLeave} onDrop={onDrop}>
                            {
                                isDragging ?
                                <>
                                    <span className='select'>Drop images here</span>
                                </> :
                                <>
                                    <p>
                                        <span className='select' onClick={selectFiles}>Click here</span> to drop media here
                                    </p>
                                </>
                            }
                            <input name="file" type="file" className='file' multiple ref={fileInputRef} onChange={onFileSelect} />
                        </div>

                        <div className='media-showcase-container'>
                            {
                                mediaFiles.map((mediaFile, index) => (
                                    mediaFile.type === 'image' ?
                                        <div className='media-showcase'>
                                            <span className='delete-media-showcase' onClick={() => removeMediaFile(index)}>&times;</span>
                                            <img src={mediaFile.url} alt={mediaFile.name} width={100}/>
                                        </div> :
                                        <div className='media-showcase'>
                                            <span className='delete-media-showcase'>&times;</span>
                                            <video src={mediaFile.url} alt={mediaFile.name} width={100}/>
                                        </div>
                                ))
                            }
                            <div className='media-showcase'>
                                <span className='delete-media-showcase'>&times;</span>
                            </div>
                        </div>
                        <span>Drag or upload images/videos. First image/video will be cover image*</span>
                    </article>
                </section>

                <section>
                    <ReactQuill theme="snow" />
                </section>
            </form>
        </FormProvider>
    </div>
  )
}

export default EventDetailForm