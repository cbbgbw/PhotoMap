import React from "react";
import classNames from "classnames";
import styles from "./Sidebar.module.scss";

export default ({ isVisible, photo }) => (
  <aside
    className={classNames(
      styles.sidebar,
      `${isVisible ? styles.sidebar_visible : null}`
    )}
  >
    {photo !== null ? (
      <>
        <h1>{photo.title}</h1>
        <p>{photo.description}</p>
        <div
          className={styles.sidebar_photoContainer}
          style={{ backgroundImage: `url(${photo.photoPath})` }}
          onClick={() => {
            var win = window.open(photo.photoPath, `_blank`);
            win.focus();
          }}
        ></div>

        <h1 className={styles.backButton}>
          <span>Go back </span>
        </h1>
      </>
    ) : null}
  </aside>
);
