import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Map, Marker, TileLayer } from "react-leaflet";
import L from "leaflet";
import { AuthUser, GetPhotos } from "./Scripts/requests";
import SvgPinIcon from "./assets/camera.svg";
import Sidebar from "./Components/Sidebar";


export default class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: true,
      isPinClicked: false,
      isAnimationOut: false,
      photos: null,
      photoPrinted: null,
    };
  }

  async componentDidMount() {
    await AuthUser();
    const respond = await GetPhotos();
    this.setState({
      photos: respond.data,
      isLoading: false,
    });
  }

  printMarkers = () => {
    const { photos } = this.state;
    return (
      <ul>
        {photos.map((photo) => {
          let icon = L.icon({
            iconUrl: SvgPinIcon,
            className: "pinIcon",
            iconSize: [255, 55],
          });
          return (
            <Marker
              key={photo.photoROWGUID}
              position={[
                photo.latitude, //temporary
                photo.longitude,
              ]}
              icon={icon}
              onclick={() => {
                if (photo !== this.state.photoPrinted) {
                  this.setState({
                    photoPrinted: photo,
                  });
                } else {
                  this.setState({
                    isPinClicked: !this.state.isPinClicked,
                    photoPrinted: photo,
                  });
                }
              }}
            ></Marker>
          );
        })}
      </ul>
    );
  };

  render() {
    const position = [51.7713362, 15.814923];
    return (
      <div className="App">
        <Map center={position} zoom={13} className="Map">
          <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
          />
          {this.state.photos !== null ? this.printMarkers() : null}
        </Map>
        <Sidebar
          isVisible={this.state.isPinClicked}
          photo={this.state.photoPrinted}
        />
      </div>
    );
  }
}
