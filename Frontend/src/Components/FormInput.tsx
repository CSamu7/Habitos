import type { InputHTMLAttributes } from "react";
import styles from "./FormInput.module.css";

interface BasicInputFormAttributes<T> extends InputHTMLAttributes<T> {
  label: string;
  error?: string;
}

export default function FormInput(
  props: BasicInputFormAttributes<HTMLInputElement>,
) {
  return (
    <div className={props.className ?? styles.formInput}>
      <label htmlFor="">{props.label}</label>
      <input {...props} />
      {<p>{props.error ?? ""}</p>}
    </div>
  );
}
