import styles from "../styles/gsaresults.module.css";

export interface GSAResultsParams {
  gsaResponse: any;
  isLoading: boolean | undefined;
}

export default function GSAResults({
  gsaResponse,
  isLoading,
}: GSAResultsParams) {
  const loadedScreen = (
    <div className={styles.resultsContainer}>
      <h2>Target Result: {gsaResponse?.targetInput ?? "n/a"}</h2>
      <h2>
        Mininum Iterations Required: {gsaResponse?.iterationsRequired ?? "n/a"}
      </h2>
    </div>
  );
  const loadingScreen = (
    <div className={styles.resultsContainer}>
      <h2>Loading...</h2>
    </div>
  );
  return isLoading ? loadingScreen : loadedScreen;
}
