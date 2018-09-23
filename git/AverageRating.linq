<Query Kind="Program" />


public void Main()
{
	Student st1 = new Student("First", 2, new int[]{1,4,3});
	Student st2 = new Student("Second", 2, new int[]{5,6,4,9});
	Student st3 = new Student("Third", 2, new int[]{9,10,3,8,5});
	Student[] stAll = new Student[]{st1, st2, st3};
	Counter count = new Counter();
	for(int i=0; i<stAll.Length; i++){
		Console.WriteLine(stAll[i].GetName() + " student's average rating " + stAll[i].AverageRating());
	}
	Console.WriteLine("All students' average rating " + Counter.AverageGroupRating(stAll));
} 

public class Student
{
	private string name;
	private int group;
	private int[] marks;
	public Student(string newName, int newGroup, int[] newMarks)
	{
		name = newName;
		group = newGroup;
		marks = newMarks;
	}
	public string GetName(){
		return name;
	}
	public int GetGroup(){
		return group;
	}
	public int[] GetMarks(){
		return marks;
	}
	public void SetName(string newName){
		name = newName;
	}
	public void SetGroup(int newGroup){
		group = newGroup;
	}
	public void SetMarks(int[] newMarks){
		marks = newMarks;
	}
	public void ShowMarks(){
		for(int i=0; i<marks.Length; i++){
			Console.WriteLine(marks[i]);
		}
	}
	public float AverageRating(){
		float rating = 0;
		for(int i=0; i< marks.Length; i++){
			rating += marks[i];
		}
		rating = rating / marks.Length;
		return rating;
	}
}

public class Counter{
	public static float AverageGroupRating(Student [] st){
		float rating = 0;
		for(int i=0; i<st.Length; i++){
			rating += st[i].AverageRating();
		}
		rating = rating / st.Length;
		return rating;
	}
}
