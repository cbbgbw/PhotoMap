import { Map, Marker, TileLayer } from "react-leaflet";
import React from "react";
import { GetPhotos, Logout, GetCountOfPhotos } from "../../Scripts/requests";
import classNames from "classnames";
import Sidebar from "../Sidebar/Sidebar";
import L from "leaflet";
import style from "./MapPage.module.scss";
import SvgPinIcon from "../../assets/camera.svg";
import { Redirect } from "react-router-dom";

export default class MapPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isPinClicked: false,
      photos: null,
      photoPrinted: null,
      logoutRequested: false,
    };
  }

  async componentWillMount() {
    setInterval(async () => {
      const { photos } = this.state;
      if (photos != null) {
        const count = await GetCountOfPhotos();
        if (count.data > photos.length) {
          this.getPhotos();
        }
      }
    }, 5000);
  }

  printMarkers = () => {
    const { photos } = this.state;
    return (
      <ul>
        {photos.map((photo) => {
          const { photoPrinted, isPinClicked } = this.state;
          let icon = L.icon({
            iconUrl: SvgPinIcon,
            className: classNames(
              style.pinIcon,
              photo === photoPrinted ? style.pinIcon_selected : null
            ),
            iconSize: [52, 55],
          });
          return (
            <Marker
              key={photo.photoROWGUID}
              position={[photo.latitude, photo.longitude]}
              icon={icon}
              onclick={() => {
                if (!isPinClicked && photoPrinted == null) {
                  this.setState({
                    isPinClicked: true,
                    photoPrinted: photo,
                  });
                } else if (isPinClicked && photoPrinted !== photo) {
                  this.setState({
                    photoPrinted: photo,
                  });
                } else if (isPinClicked && photoPrinted === photo) {
                  this.setState({
                    photoPrinted: null,
                    isPinClicked: false,
                  });
                }
              }}
            ></Marker>
          );
        })}
      </ul>
    );
  };

  async getPhotos() {
    const respond = await GetPhotos();
    this.setState({
      photos: respond.data,
      isLoading: false,
    });
  }

  async componentDidMount() {
    await this.getPhotos();
  }

  hideSidebar() {
    this.setState({
      isPinClicked: false,
      photoPrinted: null,
    });
  }

  getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min)) + min;
  }

  render() {
    const { photos } = this.state;

    let position = null;
    if (photos != null) {
      position = [
        photos[0].latitude,
        photos[0].longitude,
      ];
    }

    return (
      <>
        {this.state.logoutRequested ? <Redirect to="/login" /> : null}
        <Map center={position} zoom={13} className={style.Map}>
          <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
          />
          {this.state.photos !== null ? this.printMarkers() : null}
        </Map>
        <Sidebar
          isVisible={this.state.isPinClicked}
          photo={this.state.photoPrinted}
          hideSidebar={() => this.hideSidebar()}
        />
        <div
          className={style.Logout}
          onClick={() => {
            Logout();
            this.setState({ logoutRequested: true });
          }}
        >
          Wyloguj
        </div>
      </>
    );
  }
}
