import styles from "../styles/gsaforminputfield.module.css";

interface GSAFormInputParams {
  value: any;
  setValue: any;
  labelText: string;
  inputFieldId: string;
}

export default function GSAFormInputField({
  value,
  setValue,
  labelText,
  inputFieldId,
}: GSAFormInputParams) {
  return (
    <div className={styles.inputCell}>
      <label htmlFor={inputFieldId}>{labelText}</label>
      <br />
      <input
        type="text"
        id={inputFieldId}
        className={styles.inputField}
        value={value}
        onChange={(e) => setValue(e.target.value)}
      />
    </div>
  );
}