import styles from "./SubmitButton.module.css";

interface BasicButtonSubmitProps {
  text: string;
  className?: string;
}

export default function SubmitButton(props: BasicButtonSubmitProps) {
  return (
    <input
      type="submit"
      value={props.text}
      className={[props.className, styles.button].join(" ")}
    />
  );
}
