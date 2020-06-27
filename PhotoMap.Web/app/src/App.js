import React from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { Map, Marker, Popup, TileLayer } from "react-leaflet";
import { AuthUser, GetPhotos } from "./Scripts/requests";

export default class App extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      isLoading: true,
      photos: null,
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
        {photos.map((photo) => (
          <Marker
            key={photo.photoROWGUID}
            position={[
              photo.latitude.replace(",", "."), //temporary
              photo.longitude.replace(",", "."),
            ]}
          >
            <Popup>
              {photo.title}
              <br />
              {photo.description}
            </Popup>
          </Marker>
        ))}
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
      </div>
    );
  }
}
