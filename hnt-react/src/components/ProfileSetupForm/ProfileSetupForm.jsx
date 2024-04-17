import React, {useState, useEffect} from 'react';
import ProfileAvatar from '../../assets/images/profile-avatar.png';
import { useAuthState } from '../../context/context'
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { Input } from '../Input/Input';







const ProfileSetupForm = () => {
  const authentication = useAuthState()
  const navigate = useNavigate();
  

  //       // orgName,
  //       // orgContactNumber,
  //       // orgEmail,
  //       // orgWebsite,
  //       // orgRegistrationNumber,
  //       // contactPersonName,
  //       // contactPersonEmail,
  //       // contactPersonMobile,
  //       country,
  //       city,
  //       address,
  //       province,
  //       bio,
  //       interests
  //     };
  //   }
  const fetchProfileData = async () => {
    try {
      const response = await axios.get(
        'https://localhost:7037/api/Authentication/get-user-detail', 
        {
          headers: {
            Authorization: `Bearer ${authentication.token}`,
          },
        }
      );

      if (response.status === 200) {
        
        setProfileData(response.data);
       
      } else {
        console.error('Failed to fetch profile data:', response.statusText);
      }
    } catch (error) {
      console.error('An error occurred while fetching profile data:', error.message);
    }
  };
  
  const [activeTab, setActiveTab] = useState('individual');
  const [newInterest, setNewInterest] = useState('');
  const [profileData, setProfileData] = useState({
    fullName: '',
    dob: '',
    gender: '',
    occupation: '',
    citizenship_No: '',
    contact_No: '',
    country: '',
    city: '',
    address: '',
    province: '',
    bio: '',
    interest: ['Travelling', 'Hiking'], 
    pictureFile : ProfileAvatar, 
  });

   // updating picture
   const [pictureFile, setPictureFile] = useState(ProfileAvatar);


  

  // updating Interest
  const handleInterestKeyDown = (e) => {
    if (e.key === 'Enter' && newInterest.trim() !== '') {
      setProfileData((prevProfileData) => ({
        ...prevProfileData,
        interest: [...prevProfileData.interest, newInterest.trim()],
      }));
      setNewInterest('');
    }
  };
  
  const removeInterest = (index) => {
    setProfileData((prevProfileData) => {
      const updatedInterests = [...prevProfileData.interest];
      updatedInterests.splice(index, 1);
      return {
        ...prevProfileData,
        interest: updatedInterests,
      };
    });
  };
 
  // handling tabs
  const handleChange = (tab) => {
    setActiveTab(tab);
    
  };

  const [pictureUpdated, setPictureUpdated] = useState(false);

  // for image file
  const handleFileChange = (e) => {
    const file = e.target.files[0];

  console.log(file)

    // Update preview image
    if (file) {
      const reader = new FileReader();
      reader.onloadend = () => {
        setPictureFile(reader.result);
      };
      reader.readAsDataURL(file);

      const updatedProfileData = { ...profileData, pictureFile: file };
      setProfileData(updatedProfileData);
      setPictureUpdated(true);
    }
  };

  const handleInputChange = (field, value) => {
    
    setProfileData((prevProfileData) => ({
      ...prevProfileData,
      [field]: value,
    }));
 
  };


  const handleSaveProfile = async () => {


    let dataToSend = new FormData();

    // Append profile data
    for (const key in profileData) {
      dataToSend.append(key, profileData[key]);
    }

    dataToSend.append('PictureUpdated', pictureUpdated);
    console.log('Request Payload:', dataToSend);

    
    try {
      const response = await axios.post(
        'https://localhost:7037/api/Authentication/update-individual-user-detail',
        dataToSend,
        {
          headers: {
             'Content-Type': 'multipart/form-data',
            Authorization: `Bearer ${authentication.token}`,
          },
        }
      );

      if (response.status === 200) {
        console.log('Data saved successfully!');
        alert('Data Saved successfully');
        fetchProfileData();

        navigate('/profile-page');
      } else {
        console.error('Failed to save data:', response.statusText);
        alert('Failed to save data');
      }
    } catch (error) {
      console.error('An error occurred while saving data:', error.response.data);
    }
  };
 
  
  return (
      <section class="profile-setup-page-wrap">
        <div class="container">
          <div class="column">
            <div class="col-sm-6">
              <div class="profile-form-wrap">
              
                <form action="">
                <p>
                  <span className="image-file-input-wrap">
                    <img src={pictureFile} alt="" style={{ maxWidth: '30%', height: '30%' }} />
                  </span>
                  <label id="image-file-input-label" for="image-file-input">
                    Add profile image
                  </label>
                  <input
                    type="file"
                    id="image-file-input"
                    name="file-input"
                    // value={profileData}
                    onChange={handleFileChange}
                  />
                </p>

                

                {/* <p>
                  <span className="image-file-input-wrap">
                    <img src={pictureFile} alt="" style={{ maxWidth: '30%', height: '30%' }} />
                  </span>
                  <label id="image-file-input-label" htmlFor="image-file-input">
                    Add profile image
                  </label>
                  <Input
                    name="file-input"
                    type="file"
                    id="image-file-input"
                    defaultValue={pictureFile}
                    onChange={handleFileChange}
                  />
                </p> */}


                  <p>
                    <select class="form-select" aria-label="Default" value={profileData.country}
                        onChange={(e) => handleInputChange('country', e.target.value)}>
                      <option selected>Select Country</option>
                      <option value="Country 1">Nepal</option>
                      <option value="Country 2">India</option>
                      <option value="Country 3">America</option>
                    </select>
                    <label for="">
                      Country
                      <span class="required">*</span>
                    </label>
                  </p>



                  <div class="row">
                    <div class="col-sm-6">
                      <p>
                        <select class="form-select" aria-label="Default"  value={profileData.province}
                        onChange={(e) => handleInputChange('province', e.target.value)}>
                          <option selected>Province</option>
                          <option value="Province 1">Koshi</option>
                          <option value="Province 2">Bagmati</option>
                          <option value="Province 3">Gandaki</option>
                        </select>
                        <label for="">
                          Province
                          <span class="required">*</span>
                        </label>
                      </p>
                    </div>
                    <div class="col-sm-6">
                      <p>
                        <select class="form-select" aria-label="Default"  value={profileData.city}
                        onChange={(e) => handleInputChange('city', e.target.value)}>
                          <option selected>City</option>
                          <option value="City 1">Kahmandu</option>
                          <option value="City 2">Biratnagar</option>
                          <option value="City 3">Butwal</option>
                        </select>
                        <label for="">
                          City
                          <span class="required">*</span>
                        </label>
                      </p>
                    </div>
                  </div>
                  <p>
                    <input type="text" name="" required="" placeholder="Address"  value={profileData.address}
                        onChange={(e) => handleInputChange('address', e.target.value)} />
                    <label>
                      Address
                      <span class="required">*</span>
                    </label>
                  </p>
                  <p>
                    <textarea name="your-message" rows="4" class="" placeholder="Bio"  value={profileData.bio}
                        onChange={(e) => handleInputChange('bio', e.target.value)}/>

                   <label class="form-title-label">
                      Bio
                    </label> 
                  </p>

                  <p>
                  <div className="fome-tags-wrap">
                    {profileData.interest && profileData.interest.map((interest, index) => (
                      <div key={index} className="form-tags">
                        {interest} <span className="tags-close-icon" onClick={() => removeInterest(index)}></span>
                      </div>
                    ))}
                    <input
                      type="text"
                      placeholder="Add interest..."
                      value={newInterest}
                      onChange={(e) => setNewInterest(e.target.value)}
                      onKeyDown={handleInterestKeyDown}
                    />
                  </div>
                  <label className="form-title-label">Interests</label>
                
                  </p>
                </form>
              </div>
            </div>
            <div class="col-sm-6">
              <h2 class="entry-title">
                Hi, Please Set up your profile! 
              </h2>
              <div class="tab-wrapper">
                <nav>
                  <div class="nav nav-tabs" id="nav-tab" role="tablist">
                  <button
                    className={`nav-link ${activeTab === 'individual' ? 'active' : ''}`}
                    onClick={() => handleChange('individual')}
                  >
                    Individual
                  </button>
                  <button
                    className={`nav-link ${activeTab === 'organization' ? 'active' : ''}`}
                    onClick={() => handleChange('organization')}
                  >
                    Organization
                  </button>
                  </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                  <div class={`tab-pane fade show ${activeTab === 'individual' ? 'active' : ''}`} id="individual-profile" role="tabpanel">
                    <form action="">
                    <div class="row">

                    <div class="col-sm-6">
                    <p>
                <input
                  type="text"
                  name=""
                  required=""
                  placeholder="Full Name"
                  value={profileData.firstName}
                  onChange={(e) => handleInputChange('fullName', e.target.value)}
                />
                <label>Full Name <span className="required">*</span></label>
              </p>
                    </div>
                      

                    </div>
                      <div class="row">
                        <div class="col-sm-6">
                          <p>
                          <input
                  type="date"
                  name=""
                  required=""
                  placeholder="Date Of Birth"
                  value={profileData.dob}
                  onChange={(e) => handleInputChange('dob', e.target.value)}
                />
                            <label>Date of Birth <span class="required">*</span></label>
                          </p>
                        </div>
                        <div class="col-sm-6">
                          <p>
                            <select class="form-select" aria-label="Default"  value={profileData.gender}
                        onChange={(e) => handleInputChange('gender', e.target.value)}>
                              <option selected="">Gender</option>
                              <option value="male">male</option>
                              <option value="female">female</option>
                            </select>
                            <label for="">
                              Gender
                              <span class="required">*</span>
                            </label>
                          </p>
                        </div>
                      </div>
                      {/* <p>
                        <input type="text" name="" required="" placeholder="Contact Number" />
                        <label>Contact Number <span class="required">*</span></label>
                      </p> */}
                      <p>
                        <input type="text" name="" required="" placeholder="Occupation"   value={profileData.occupation}
                        onChange={(e) => handleInputChange('occupation', e.target.value)}/>
                        <label>Occupation <span class="required">*</span></label>
                      </p>

                      <p>
                        <input type="text" name="" required="" placeholder="Citizenship/Passport No" value={profileData.citizenship_No}
                        onChange={(e) => handleInputChange('citizenship_No', e.target.value)}/>
                        <label>Citizenship/Passport No <span class="required">*</span></label>
                      </p>

                      <p>
                        <input type="text" name="" required="" placeholder="Contact No"  value={profileData.contact_No}
                        onChange={(e) => handleInputChange('contact_No', e.target.value)}/>
                        <label>contactNo <span class="required">*</span></label>
                      </p>
                      
                      {/* <p>
                        <input type="text" name="" required="" placeholder="Education" />
                        <label>Education <span class="required">*</span></label>
                      </p> */}
                      <p class="submit-btn-wrap">
                      <button type="button" className="box-button" onClick={handleSaveProfile}>
                       Save
                      </button>
                        <a href="login-page" class="box-button only-border-btn">skip</a>
                      </p>
                    </form>
                  </div>
                  <div class={`tab-pane fade show ${activeTab === 'organization' ? 'active' : ''}`} id="organization-profile" role="tabpanel">
                    <form action="">
                      <p>
                        <input type="text" name="" required="" placeholder="Organization Name" />
                        <label>Organization Name <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="text" name="" required="" placeholder="Organization Contact Number" />
                        <label>Organization Contact Number <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="email" name="" required="" placeholder="Organization Email" />
                        <label>Organization Email <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="text" name="" required="" placeholder="Organization Website Link" />
                        <label>Organization Website Link <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="text" name="" required="" placeholder="Organization Registration Number" />
                        <label>Organization Registration Number <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="text" name="" required="" placeholder="Contact person Name" />
                        <label>Contact Person Name <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="text" name="" required="" placeholder="Contact person" />
                        <label>Contact Person Email <span class="required">*</span></label>
                      </p>
                      <p>
                        <input type="text" name="" required="" placeholder="Contact Person Mobile No" />
                        <label>Contact Person Mobile No <span class="required">*</span></label>
                      </p>
                      <p class="submit-btn-wrap">
                      <button type="button" className="box-button" onClick={handleSaveProfile}>
                       Save
                      </button>
                        <a href="#" class="box-button only-border-btn">skip</a>
                      </p>
                    </form>
                  </div>
                </div>

              </div>
            </div>
          </div>
        </div>
      </section>
  );
}

export default ProfileSetupForm;
