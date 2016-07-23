using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Skahal.Serialization;

namespace Skahal.Serialization.UnitTests
{
    [Category("Serialization")]
    public class SHSerializerTest
    {
        [Test]
        public void SerializeToString_SerializeString_RightDeserialized()
        {
            var actual = SHSerializer.SerializeToString("teste");
            var deserialized = SHSerializer.DeserializeFromString<string>(actual);

            Assert.AreEqual("teste", deserialized);
        }

        [Test]
        public void SerializeToString_SerializeObject_RightDeserialized()
        {
            var stub = new SerializationTargetStub
            {
                Boolean = true,
                Integer = 123,
                Text = "teste"
            };

            var actual = SHSerializer.SerializeToString(stub);
            var deserialized = SHSerializer.DeserializeFromString<SerializationTargetStub>(actual);

            Assert.IsTrue(deserialized.Boolean);
            Assert.AreEqual(123, deserialized.Integer);
            Assert.AreEqual("teste", deserialized.Text);
        }

		[Test]
		public void SerializeVector2_Vector2_RightDeserialized()
		{
			var actual = SHSerializer.SerializeVector2(new Vector2(1.2f, 3.4f));
			var deserialized = SHSerializer.DeserializeVector2(actual);

			Assert.AreEqual(1.2f, deserialized.x);
			Assert.AreEqual(3.4f, deserialized.y);
		}

		[Test]
		public void SerializeVector3_Vector3_RightDeserialized()
		{
			var actual = SHSerializer.SerializeVector3(new Vector3(1.2f, 3.4f, 5.6f));
			var deserialized = SHSerializer.DeserializeVector3(actual);

			Assert.AreEqual(1.2f, deserialized.x);
			Assert.AreEqual(3.4f, deserialized.y);
			Assert.AreEqual(5.6f, deserialized.z);
		}

        [Test]
		[Category("Unity")]
        public void SerializeToString_SerializeObjectUsingPlayerPrefs_RightDeserialized()
        {
            var stub = new SerializationTargetStub
            {
                Boolean = true,
                Integer = 123,
                Text = "teste"
            };

            var actual = SHSerializer.SerializeToString(stub);
            PlayerPrefs.SetString("SerializeToString_SerializeObjectUsingPlayerPrefs_RightDeserialized", actual);
            var storaged = PlayerPrefs.GetString("SerializeToString_SerializeObjectUsingPlayerPrefs_RightDeserialized");

            var deserialized = SHSerializer.DeserializeFromString<SerializationTargetStub>(storaged);

            Assert.IsTrue(deserialized.Boolean);
            Assert.AreEqual(123, deserialized.Integer);
            Assert.AreEqual("teste", deserialized.Text);
        }
    }
}