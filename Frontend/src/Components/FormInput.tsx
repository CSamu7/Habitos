import type { InputHTMLAttributes } from "react";
import styles from "./FormInput.module.css";

interface BasicInputFormAttributes<T> extends InputHTMLAttributes<T> {
  label: string;
  error?: string;
  textinformation?: string;
}

export default function FormInput(
  props: BasicInputFormAttributes<HTMLInputElement>,
) {
  return (
    <div className={props.className ?? styles.formInput}>
      <label htmlFor="">{props.label}</label>
      {props.textinformation && (
        <p className={styles.inputInfo}>{props.textinformation}</p>
      )}
      <input {...props} />
      {<p className={styles.error}>{props.error ?? ""}</p>}
    </div>
  );
}
