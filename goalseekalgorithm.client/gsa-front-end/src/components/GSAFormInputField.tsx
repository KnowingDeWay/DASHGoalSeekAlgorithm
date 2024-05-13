import styles from "../styles/gsaforminputfield.module.css";

interface GSAFormInputParams {
  value: any;
  setValue: any;
  labelText: string;
  inputFieldId: string;
  inputType: string;
}

export default function GSAFormInputField({
  value,
  setValue,
  labelText,
  inputFieldId,
  inputType,
}: GSAFormInputParams) {
  return (
    <div className={styles.inputCell}>
      <label htmlFor={inputFieldId}>{labelText}</label>
      <br />
      <input
        type={inputType}
        id={inputFieldId}
        className={styles.inputField}
        value={value}
        onChange={(e) => setValue(e.target.value)}
      />
    </div>
  );
}
