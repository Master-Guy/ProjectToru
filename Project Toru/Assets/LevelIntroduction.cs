using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroduction : MonoBehaviour
{

	public SpriteRenderer backgroundBlack;
	public SpriteRenderer background;

	public TextMesh textMesh;
	public TextMesh textMeshControls;

	public List<string> text = new List<string>();

	State state = State.BlackFadeIn;

	float WaitTimer = 1;
	int currentLine = 0;

    // Start is called before the first frame update
    void Start()
    {
		backgroundBlack.gameObject.SetActive(true);
		background.gameObject.SetActive(true);

		textMesh.GetComponent<Renderer>().sortingLayerName = "UI";
		textMeshControls.GetComponent<Renderer>().sortingLayerName = "UI";
    }

	enum State {
		None,
		Start,
		BlackFadeIn,
		BackgroundFadeOut,
		FadeInText,
		SelectNextLine,
		FadeOutText,
		Wait,
		FadeOutIntroduction,
		WaitForInput,
		FadeInControls
	};

	

    // Update is called once per frame
    void Update()
    {
        switch(state) {

			case State.Start:
				state = State.BlackFadeIn;
			break;

			case State.BlackFadeIn:
				{
					Color color = backgroundBlack.color;
					color.r += 0.5f * Time.deltaTime;
					color.g += 0.5f * Time.deltaTime;
					color.b += 0.5f * Time.deltaTime;
					backgroundBlack.color = color;
					
					if (color.r >= 1) state = State.BackgroundFadeOut;
				}
			break;

			case State.BackgroundFadeOut:
				{
					Color color = background.color;
					color.a -= 0.5f * Time.deltaTime;
					background.color = color;
					
					if (color.a <= 0) state = State.SelectNextLine;
				}
			break;

			case State.FadeInText:
				{
					Color color = textMesh.color;
					color.a += 1f * Time.deltaTime;
					textMesh.color = color;
					
					if (color.a >= 1) {
						if (currentLine == 1) {
							state = State.FadeInControls;
						}
						else {
							state = State.WaitForInput;
						}
					}
				}
			break;

			case State.FadeInControls:
				{
					Color color = textMeshControls.color;
					color.a += 1f * Time.deltaTime;
					textMeshControls.color = color;
					
					if (color.a >= 1) state = State.WaitForInput;
				}
			break;

			case State.Wait:
				{
					WaitTimer -= Time.deltaTime;
					
					if (WaitTimer <= 0) state = State.None;
				}
			break;

			case State.SelectNextLine:
				{
					if (currentLine >= text.Count) {
						state = State.FadeOutIntroduction;
						break;
					}

					textMesh.text = text[currentLine];
					currentLine++;

					state = State.FadeInText;
				}
			break;

			case State.FadeOutText:
				{
					Color color = textMesh.color;
					color.a -= 1f * Time.deltaTime;
					textMesh.color = color;
					
					if (color.a <= 0) state = State.SelectNextLine;
				}
			break;

			case State.WaitForInput:
				{
					if (Input.GetKeyDown(KeyCode.Space)) {
						state = State.FadeOutText;
					}
				}
			break;

			case State.FadeOutIntroduction: {
				{
					Color color = textMesh.color;
					color.a -= 0.5f * Time.deltaTime;
					textMesh.color = color;
				}

				{
					Color color = textMeshControls.color;
					color.a -= 1f * Time.deltaTime;
					textMeshControls.color = color;
				}

				{
					Color color = backgroundBlack.color;
					color.a -= 0.3f * Time.deltaTime;
					backgroundBlack.color = color;
					
					if (color.a <= 0) gameObject.SetActive(false);
				}
			}
			break;

			

			default:
				break;
		}
    }
}
